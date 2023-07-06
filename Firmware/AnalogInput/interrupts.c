#include "cpu.h"
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"
#include "app_funcs.h"
#include "hwbp_core.h"

/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;

/************************************************************************/
/* Interrupts from Timers                                               */
/************************************************************************/
// ISR(TCC0_OVF_vect, ISR_NAKED)
// ISR(TCD0_OVF_vect, ISR_NAKED)
// ISR(TCE0_OVF_vect, ISR_NAKED)
// ISR(TCF0_OVF_vect, ISR_NAKED)
// 
// ISR(TCC0_CCA_vect, ISR_NAKED)
// ISR(TCD0_CCA_vect, ISR_NAKED)
// ISR(TCE0_CCA_vect, ISR_NAKED)
// ISR(TCF0_CCA_vect, ISR_NAKED)
// 
// ISR(TCD1_OVF_vect, ISR_NAKED)
// 
// ISR(TCD1_CCA_vect, ISR_NAKED)

/************************************************************************/ 
/* DI0                                                                  */
/************************************************************************/
bool previous_DIO = false;
ISR(PORTB_INT0_vect, ISR_NAKED)
{
	if (read_DI0)
	{
		if (!previous_DIO)
		{
			previous_DIO = true;
			
			switch (app_regs.REG_DI0_CONF)
			{
				case GM_DI0_SYNC:
					app_regs.REG_DI0 |= B_DI0;
					core_func_send_event(ADD_REG_DI0, true);
					break;				
				
				case GM_DI0_RISE_START_ACQ:
					app_regs.REG_START = 1;
					break;								
				
				case GM_DI0_FALL_START_ACQ:
					app_regs.REG_START = 0;
					break;
				
				case GM_DI0_RISE_CATCH_SAMPLE:
					set_CONVSTA;
					set_CONVSTB;
					switch (app_regs.REG_TRIGGER_DESTINY)
					{
						case GM_TRIG_TO_DO0: set_DO0; break;
						case GM_TRIG_TO_DO1: set_DO1; break;
						case GM_TRIG_TO_DO2: set_DO2; break;
						case GM_TRIG_TO_DO3: set_DO3; break;
					}
					break;
			}
		}
	}
	else
	{
		if (previous_DIO)
		{
			previous_DIO = false;
			
			switch (app_regs.REG_DI0_CONF)
			{
				case GM_DI0_SYNC:
				app_regs.REG_DI0 &= ~B_DI0;
				core_func_send_event(ADD_REG_DI0, true);
				break;
				
				case GM_DI0_RISE_START_ACQ:
					app_regs.REG_START = 0;
					break;
				
				case GM_DI0_FALL_START_ACQ:
					app_regs.REG_START = 1;
					break;
			}
		}
	}	
	
	reti();
}

/************************************************************************/ 
/* BUSY                                                                 */
/************************************************************************/
void process_thresholds(void);

ISR(PORTD_INT0_vect, ISR_NAKED)
{
	if (!read_BUSY)
	{
		clr_CONVSTA;
		clr_CONVSTB;
		
		switch (app_regs.REG_TRIGGER_DESTINY)
		{
			case GM_TRIG_TO_DO0: clr_DO0; break;
			case GM_TRIG_TO_DO1: clr_DO1; break;
			case GM_TRIG_TO_DO2: clr_DO2; break;
			case GM_TRIG_TO_DO3: clr_DO3; break;
		}
		
		set_CS_ADC;
		
		for (uint8_t i = 0; i < 4; i++)
		{
			SPIC_DATA = 0;
			loop_until_bit_is_set(SPIC_STATUS, SPI_IF_bp);         
			*(((uint8_t*)(&app_regs.REG_ANALOG_INPUTS[0])) + i*2 + 1) = SPIC_DATA;
         
			SPIC_DATA = 0;
			loop_until_bit_is_set(SPIC_STATUS, SPI_IF_bp);
			*(((uint8_t*)(&app_regs.REG_ANALOG_INPUTS[0])) + i*2 + 0) = SPIC_DATA;
		}
		
		clr_CS_ADC;
		
		core_func_send_event(ADD_REG_ANALOG_INPUTS, false);
		process_thresholds();
	}
	
	reti();
}

/************************************************************************/
/* Process Thresholds                                                   */
/************************************************************************/
uint16_t ch0_up_counter = 0;
uint16_t ch1_up_counter = 0;
uint16_t ch2_up_counter = 0;
uint16_t ch3_up_counter = 0;
uint16_t ch0_down_counter = 0;
uint16_t ch1_down_counter = 0;
uint16_t ch2_down_counter = 0;
uint16_t ch3_down_counter = 0;

#define NEW_DOUT_NO_CHANGE 0
#define NEW_DOUT_TO_HIGH 1
#define NEW_DOUT_TO_LOW 2

/* This functions needs 52 us when using the 4 thresholds */
void process_thresholds(void)
{	
	bool send_event = false;
	
	/* Clear changed flags and update register to current values */
	app_regs.REG_DO_WRITE = PORTA_IN & 0x0F;	
	
	/* Output channel 0 */
	if ((app_regs.REG_DO0_CH != GM_NOT_USED) && (app_regs.REG_DO0_CONF != GM_DO0_TGL_EACH_SEC))
	{
		if (app_regs.REG_ANALOG_INPUTS[app_regs.REG_DO0_CH] >= app_regs.REG_DO0_TH_VALUE)
		{
			if (++ch0_up_counter == app_regs.REG_DO0_TH_UP_SAMPLES + 1)
			{
				send_event = true;
				app_regs.REG_DO_WRITE |= (B_DO0_CHANGED | B_DO0);
				set_DO0;
			}
			
			if (ch0_up_counter > app_regs.REG_DO0_TH_UP_SAMPLES)
				ch0_up_counter = app_regs.REG_DO0_TH_UP_SAMPLES + 1;
			
			ch0_down_counter = 0;
		}
		else
		{
			if (++ch0_down_counter == app_regs.REG_DO0_TH_DOWN_SAMPLES + 1)
			{
				send_event = true;
				app_regs.REG_DO_WRITE |= B_DO0_CHANGED;
				app_regs.REG_DO_WRITE &= ~B_DO0;
				clr_DO0;
			}
			
			if (ch0_down_counter > app_regs.REG_DO0_TH_DOWN_SAMPLES)
				ch0_down_counter = app_regs.REG_DO0_TH_DOWN_SAMPLES + 1;
			
			ch0_up_counter = 0;
		}
	}
	
	/* Output channel 1 */
	if (app_regs.REG_DO1_CH != GM_NOT_USED)
	{
		if (app_regs.REG_ANALOG_INPUTS[app_regs.REG_DO1_CH] >= app_regs.REG_DO1_TH_VALUE)
		{
			if (++ch1_up_counter == app_regs.REG_DO1_TH_UP_SAMPLES + 1)
			{
				send_event = true;
				app_regs.REG_DO_WRITE |= (B_DO1_CHANGED | B_DO1);
				set_DO1;
			}
			
			if (ch1_up_counter > app_regs.REG_DO1_TH_UP_SAMPLES)
				ch1_up_counter = app_regs.REG_DO1_TH_UP_SAMPLES + 1;
			
			ch1_down_counter = 0;
		}
		else
		{
			if (++ch1_down_counter == app_regs.REG_DO1_TH_DOWN_SAMPLES + 1)
			{
				send_event = true;
				app_regs.REG_DO_WRITE |= B_DO1_CHANGED;
				app_regs.REG_DO_WRITE &= ~B_DO1;
				clr_DO1;
			}
			
			if (ch1_down_counter > app_regs.REG_DO1_TH_DOWN_SAMPLES)
				ch1_down_counter = app_regs.REG_DO1_TH_UP_SAMPLES + 1;
			
			ch1_up_counter = 0;
		}
	}
	
	/* Output channel 2 */
	if (app_regs.REG_DO2_CH != GM_NOT_USED)
	{
		if (app_regs.REG_ANALOG_INPUTS[app_regs.REG_DO2_CH] >= app_regs.REG_DO2_TH_VALUE)
		{
			if (++ch2_up_counter == app_regs.REG_DO2_TH_UP_SAMPLES + 1)
			{
				send_event = true;
				app_regs.REG_DO_WRITE |= (B_DO2_CHANGED | B_DO2);
				set_DO2;
			}
			
			if (ch2_up_counter > app_regs.REG_DO2_TH_UP_SAMPLES)
				ch2_up_counter = app_regs.REG_DO2_TH_UP_SAMPLES + 1;
			
			ch2_down_counter = 0;
		}
		else
		{
			if (++ch2_down_counter == app_regs.REG_DO2_TH_DOWN_SAMPLES + 1)
			{
				send_event = true;
				app_regs.REG_DO_WRITE |= B_DO2_CHANGED;
				app_regs.REG_DO_WRITE &= ~B_DO2;
				clr_DO2;
			}
			
			if (ch2_down_counter > app_regs.REG_DO2_TH_DOWN_SAMPLES)
				ch2_down_counter = app_regs.REG_DO2_TH_DOWN_SAMPLES + 1;
			
			ch2_up_counter = 0;
		}
	}
	
	/* Output channel 3 */
	if (app_regs.REG_DO3_CH != GM_NOT_USED)
	{
		if (app_regs.REG_ANALOG_INPUTS[app_regs.REG_DO3_CH] >= app_regs.REG_DO3_TH_VALUE)
		{
			if (++ch3_up_counter == app_regs.REG_DO3_TH_UP_SAMPLES + 1)
			{
				send_event = true;
				app_regs.REG_DO_WRITE |= (B_DO3_CHANGED | B_DO3);
				set_DO3;
			}
			if (ch3_up_counter > app_regs.REG_DO3_TH_UP_SAMPLES)
				ch3_up_counter = app_regs.REG_DO3_TH_UP_SAMPLES + 1;
			
			ch3_down_counter = 0;
		}
		else
		{
			if (++ch3_down_counter == app_regs.REG_DO3_TH_DOWN_SAMPLES + 1)
			{
				send_event = true;
				app_regs.REG_DO_WRITE |= B_DO3_CHANGED;
				app_regs.REG_DO_WRITE &= ~B_DO3;
				clr_DO3;
			}
			
			if (ch3_down_counter > app_regs.REG_DO3_TH_DOWN_SAMPLES)
				ch3_down_counter = app_regs.REG_DO3_TH_DOWN_SAMPLES + 1;
			
			ch3_up_counter = 0;
		}
	}
	
	if (send_event)
	{
		core_func_send_event(ADD_REG_DO_WRITE, false);
	}
}