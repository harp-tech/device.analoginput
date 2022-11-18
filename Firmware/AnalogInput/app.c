#include "hwbp_core.h"
#include "hwbp_core_regs.h"
#include "hwbp_core_types.h"

#include "app.h"
#include "app_funcs.h"
#include "app_ios_and_regs.h"

#define F_CPU 32000000
#include <util/delay.h>

/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;
extern uint8_t app_regs_type[];
extern uint16_t app_regs_n_elements[];
extern uint8_t *app_regs_pointer[];
extern void (*app_func_rd_pointer[])(void);
extern bool (*app_func_wr_pointer[])(void*);

/************************************************************************/
/* Initialize app                                                       */
/************************************************************************/
static const uint8_t default_device_name[] = "AnalogInput";

void hwbp_app_initialize(void)
{
    /* Define versions */
    uint8_t hwH = 1;
    uint8_t hwL = 0;
    uint8_t fwH = 1;
    uint8_t fwL = 0;
    uint8_t ass = 0;
    
   	/* Start core */
    core_func_start_core(
        1236,
        hwH, hwL,
        fwH, fwL,
        ass,
        (uint8_t*)(&app_regs),
        APP_NBYTES_OF_REG_BANK,
        APP_REGS_ADD_MAX - APP_REGS_ADD_MIN + 1,
        default_device_name
    );
}

/************************************************************************/
/* Handle if a catastrophic error occur                                 */
/************************************************************************/
void core_callback_catastrophic_error_detected(void)
{
	
}

/************************************************************************/
/* User functions                                                       */
/************************************************************************/
/* Add your functions here or load external functions if needed */

/************************************************************************/
/* Initialization Callbacks                                             */
/************************************************************************/
void core_callback_1st_config_hw_after_boot(void)
{
	/* Initialize IOs */
	/* Don't delete this function!!! */
	init_ios();
	
	/* Initialize hardware */
	
	/* Initialize SPI with 4MHz */
	SPIC_CTRL = SPI_MASTER_bm | SPI_ENABLE_bm | SPI_MODE_0_gc | SPI_CLK2X_bm | SPI_PRESCALER_DIV16_gc;
	
	/* Reset ADC */
	_delay_ms(100);
	set_RESET;
	_delay_ms(1);
	clr_RESET;
	_delay_ms(1);		
}

void core_callback_reset_registers(void)
{
	/* Initialize registers */
	app_regs.REG_START = 0;
	
	app_regs.REG_RANGE_AND_INPUT_FILTER = GM_10V_1K5;
	app_regs.REG_SAMPLE_FREQUENCY = GM_1KHZ;
	
	app_regs.REG_DI0_CONF = GM_DI0_SYNC;
	
	app_regs.REG_DO0_CONF = GM_DO0_DIG;
	app_regs.REG_DO0_PULSE = 10;
	
	app_regs.REG_TRIGGER_DESTINY = GM_TRIG_TO_NONE;
	
	app_regs.REG_DO0_CH = GM_NOT_USED;
	app_regs.REG_DO1_CH = GM_NOT_USED;
	app_regs.REG_DO2_CH = GM_NOT_USED;
	app_regs.REG_DO3_CH = GM_NOT_USED;
	app_regs.REG_DO0_TH_VALUE = 0;
	app_regs.REG_DO1_TH_VALUE = 0;
	app_regs.REG_DO2_TH_VALUE = 0;
	app_regs.REG_DO3_TH_VALUE = 0;
	app_regs.REG_DO0_TH_UP_SAMPLES = 1;
	app_regs.REG_DO1_TH_UP_SAMPLES = 1;
	app_regs.REG_DO2_TH_UP_SAMPLES = 1;
	app_regs.REG_DO3_TH_UP_SAMPLES = 1;
	app_regs.REG_DO0_TH_DOWN_SAMPLES = 1;
	app_regs.REG_DO1_TH_DOWN_SAMPLES = 1;
	app_regs.REG_DO2_TH_DOWN_SAMPLES = 1;
	app_regs.REG_DO3_TH_DOWN_SAMPLES = 1;
}

void core_callback_registers_were_reinitialized(void)
{
	/* Update registers if needed */
	app_write_REG_DO_WRITE(&app_regs.REG_DO_WRITE);
	
	app_write_REG_RANGE_AND_INPUT_FILTER(&app_regs.REG_RANGE_AND_INPUT_FILTER);
	
//		app_regs.REG_DO0_CH = GM_ANA3;
// 		app_regs.REG_DO1_CH = GM_ANA1;
 		app_regs.REG_DO2_CH = GM_ANA3;
// 		app_regs.REG_DO3_CH = GM_ANA3;
		app_regs.REG_DO0_TH_VALUE = 5000;
		app_regs.REG_DO1_TH_VALUE = 5000;
		app_regs.REG_DO2_TH_VALUE = 5000;
		app_regs.REG_DO3_TH_VALUE = 5000;
}

/************************************************************************/
/* Callbacks: Visualization                                             */
/************************************************************************/
void core_callback_visualen_to_on(void)
{
	/* Update visual indicators */
	
}

void core_callback_visualen_to_off(void)
{
	/* Clear all the enabled indicators */
	
}

/************************************************************************/
/* Callbacks: Change on the operation mode                              */
/************************************************************************/
void core_callback_device_to_standby(void)
{
	app_regs.REG_START = 0;
}
void core_callback_device_to_active(void) {}
void core_callback_device_to_enchanced_active(void) {}
void core_callback_device_to_speed(void) {}

/************************************************************************/
/* Callbacks: 1 ms timer                                                */
/************************************************************************/
uint16_t second_counter = 0;
uint16_t pulse_counter_ms = 0;

void core_callback_t_before_exec(void)
{
	if (++second_counter == 2000)
	{
		if (app_regs.REG_START)
		{
			if (app_regs.REG_DO0_CONF == GM_DO0_TGL_EACH_SEC)
			{
				if (read_DO0)
				{
					clr_DO0;
					app_regs.REG_DO_WRITE &= ~B_DO0;
				}
				else
				{
					set_DO0;
					app_regs.REG_DO_WRITE |= B_DO0;
				}
			}
		}
	}
}
void core_callback_t_after_exec(void) {}
void core_callback_t_new_second(void)
{
	second_counter = 0;
}
void core_callback_t_500us(void)
{
	/* Read ADC if 2Khz sample rate is selected */
	if (app_regs.REG_DI0_CONF != GM_DI0_RISE_CATCH_SAMPLE)
	{
		if (app_regs.REG_START)
		{
			if (app_regs.REG_SAMPLE_FREQUENCY == GM_2KHZ)
			{
				core_func_mark_user_timestamp();
				set_CONVST;
				set_CONVSTB;
				switch (app_regs.REG_TRIGGER_DESTINY)
				{
					case GM_TRIG_TO_DO0: set_DO0; break;
					case GM_TRIG_TO_DO1: set_DO1; break;
					case GM_TRIG_TO_DO2: set_DO2; break;
					case GM_TRIG_TO_DO3: set_DO3; break;
				}
			}
		}
	}
	
	/* Pulse on DO0 */
	if (pulse_counter_ms)
	{
		if (!(--pulse_counter_ms))
		{
			if (app_regs.REG_DO0_CONF == GM_DO0_PULSE)
			{
				clr_DO0;
				app_regs.REG_DO_WRITE &= ~B_DO0;
			}            
		}
	}
}
void core_callback_t_1ms(void)
{
	/* Read ADC */
	if (app_regs.REG_DI0_CONF != GM_DI0_RISE_CATCH_SAMPLE)
	{
		if (app_regs.REG_START)
		{
			core_func_mark_user_timestamp();		
			set_CONVST;
			set_CONVSTB;
			switch (app_regs.REG_TRIGGER_DESTINY)
			{
				case GM_TRIG_TO_DO0: set_DO0; break;
				case GM_TRIG_TO_DO1: set_DO1; break;
				case GM_TRIG_TO_DO2: set_DO2; break;
				case GM_TRIG_TO_DO3: set_DO3; break;
			}
		}
	}
}

/************************************************************************/
/* Callbacks: uart control                                              */
/************************************************************************/
void core_callback_uart_rx_before_exec(void) {}
void core_callback_uart_rx_after_exec(void) {}
void core_callback_uart_tx_before_exec(void) {}
void core_callback_uart_tx_after_exec(void) {}
void core_callback_uart_cts_before_exec(void) {}
void core_callback_uart_cts_after_exec(void) {}

/************************************************************************/
/* Callbacks: Read app register                                         */
/************************************************************************/
bool core_read_app_register(uint8_t add, uint8_t type)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;
	
	/* Receive data */
	(*app_func_rd_pointer[add-APP_REGS_ADD_MIN])();	

	/* Return success */
	return true;
}

/************************************************************************/
/* Callbacks: Write app register                                        */
/************************************************************************/
bool core_write_app_register(uint8_t add, uint8_t type, uint8_t * content, uint16_t n_elements)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;

	/* Check if the number of elements matches */
	if (app_regs_n_elements[add-APP_REGS_ADD_MIN] != n_elements)
		return false;

	/* Process data and return false if write is not allowed or contains errors */
	return (*app_func_wr_pointer[add-APP_REGS_ADD_MIN])(content);
}