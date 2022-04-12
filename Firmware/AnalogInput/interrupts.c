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
ISR(PORTB_INT0_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/ 
/* BUSY                                                                 */
/************************************************************************/
ISR(PORTD_INT0_vect, ISR_NAKED)
{
	if (!read_BUSY)
	{
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
				
		clr_CONVST;
		
		core_func_send_event(ADD_REG_ANALOG_INPUTS, false);
	}
	
	reti();
}

