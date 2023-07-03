#include "app_funcs.h"
#include "app_ios_and_regs.h"
#include "hwbp_core.h"

extern uint16_t pulse_counter_ms;

/************************************************************************/
/* Create pointers to functions                                         */
/************************************************************************/
extern AppRegs app_regs;

void (*app_func_rd_pointer[])(void) = {
	&app_read_REG_START,
	&app_read_REG_ANALOG_INPUTS,
	&app_read_REG_DI0,
	&app_read_REG_RESERVED0,
	&app_read_REG_RESERVED00,
	&app_read_REG_RANGE_AND_INPUT_FILTER,
	&app_read_REG_SAMPLE_FREQUENCY,
	&app_read_REG_DI0_CONF,
	&app_read_REG_DO0_CONF,
	&app_read_REG_DO0_PULSE,
	&app_read_REG_DO_SET,
	&app_read_REG_DO_CLEAR,
	&app_read_REG_DO_TOGGLE,
	&app_read_REG_DO_WRITE,
	&app_read_REG_RESERVED1,
	&app_read_REG_RESERVED2,
	&app_read_REG_TRIGGER_DESTINY,
	&app_read_REG_RESERVED3,
	&app_read_REG_RESERVED4,
	&app_read_REG_RESERVED5,
	&app_read_REG_RESERVED6,
	&app_read_REG_RESERVED7,
	&app_read_REG_RESERVED8,
	&app_read_REG_RESERVED9,
	&app_read_REG_RESERVED10,
	&app_read_REG_RESERVED11,
	&app_read_REG_DO0_CH,
	&app_read_REG_DO1_CH,
	&app_read_REG_DO2_CH,
	&app_read_REG_DO3_CH,
	&app_read_REG_RESERVED12,
	&app_read_REG_RESERVED13,
	&app_read_REG_RESERVED14,
	&app_read_REG_RESERVED15,
	&app_read_REG_DO0_TH_VALUE,
	&app_read_REG_DO1_TH_VALUE,
	&app_read_REG_DO2_TH_VALUE,
	&app_read_REG_DO3_TH_VALUE,
	&app_read_REG_RESERVED17,
	&app_read_REG_RESERVED18,
	&app_read_REG_RESERVED19,
	&app_read_REG_RESERVED20,
	&app_read_REG_DO0_TH_UP_SAMPLES,
	&app_read_REG_DO1_TH_UP_SAMPLES,
	&app_read_REG_DO2_TH_UP_SAMPLES,
	&app_read_REG_DO3_TH_UP_SAMPLES,
	&app_read_REG_RESERVED21,
	&app_read_REG_RESERVED22,
	&app_read_REG_RESERVED23,
	&app_read_REG_RESERVED24,
	&app_read_REG_DO0_TH_DOWN_SAMPLES,
	&app_read_REG_DO1_TH_DOWN_SAMPLES,
	&app_read_REG_DO2_TH_DOWN_SAMPLES,
	&app_read_REG_DO3_TH_DOWN_SAMPLES,
	&app_read_REG_RESERVED25,
	&app_read_REG_RESERVED26,
	&app_read_REG_RESERVED27,
	&app_read_REG_RESERVED28,
	&app_read_REG_RESERVED29
};

bool (*app_func_wr_pointer[])(void*) = {
	&app_write_REG_START,
	&app_write_REG_ANALOG_INPUTS,
	&app_write_REG_DI0,
	&app_write_REG_RESERVED0,
	&app_write_REG_RESERVED00,
	&app_write_REG_RANGE_AND_INPUT_FILTER,
	&app_write_REG_SAMPLE_FREQUENCY,
	&app_write_REG_DI0_CONF,
	&app_write_REG_DO0_CONF,
	&app_write_REG_DO0_PULSE,
	&app_write_REG_DO_SET,
	&app_write_REG_DO_CLEAR,
	&app_write_REG_DO_TOGGLE,
	&app_write_REG_DO_WRITE,
	&app_write_REG_RESERVED1,
	&app_write_REG_RESERVED2,
	&app_write_REG_TRIGGER_DESTINY,
	&app_write_REG_RESERVED3,
	&app_write_REG_RESERVED4,
	&app_write_REG_RESERVED5,
	&app_write_REG_RESERVED6,
	&app_write_REG_RESERVED7,
	&app_write_REG_RESERVED8,
	&app_write_REG_RESERVED9,
	&app_write_REG_RESERVED10,
	&app_write_REG_RESERVED11,
	&app_write_REG_DO0_CH,
	&app_write_REG_DO1_CH,
	&app_write_REG_DO2_CH,
	&app_write_REG_DO3_CH,
	&app_write_REG_RESERVED12,
	&app_write_REG_RESERVED13,
	&app_write_REG_RESERVED14,
	&app_write_REG_RESERVED15,
	&app_write_REG_DO0_TH_VALUE,
	&app_write_REG_DO1_TH_VALUE,
	&app_write_REG_DO2_TH_VALUE,
	&app_write_REG_DO3_TH_VALUE,
	&app_write_REG_RESERVED17,
	&app_write_REG_RESERVED18,
	&app_write_REG_RESERVED19,
	&app_write_REG_RESERVED20,
	&app_write_REG_DO0_TH_UP_SAMPLES,
	&app_write_REG_DO1_TH_UP_SAMPLES,
	&app_write_REG_DO2_TH_UP_SAMPLES,
	&app_write_REG_DO3_TH_UP_SAMPLES,
	&app_write_REG_RESERVED21,
	&app_write_REG_RESERVED22,
	&app_write_REG_RESERVED23,
	&app_write_REG_RESERVED24,
	&app_write_REG_DO0_TH_DOWN_SAMPLES,
	&app_write_REG_DO1_TH_DOWN_SAMPLES,
	&app_write_REG_DO2_TH_DOWN_SAMPLES,
	&app_write_REG_DO3_TH_DOWN_SAMPLES,
	&app_write_REG_RESERVED25,
	&app_write_REG_RESERVED26,
	&app_write_REG_RESERVED27,
	&app_write_REG_RESERVED28,
	&app_write_REG_RESERVED29
};


/************************************************************************/
/* REG_START                                                            */
/************************************************************************/
void app_read_REG_START(void)
{
	//app_regs.REG_START = 0;

}

bool app_write_REG_START(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_START = reg;
	return true;
}


/************************************************************************/
/* REG_ANALOG_INPUTS                                                    */
/************************************************************************/
// This register is an array with 4 positions
void app_read_REG_ANALOG_INPUTS(void)
{
	//app_regs.REG_ANALOG_INPUTS[0] = 0;

}

bool app_write_REG_ANALOG_INPUTS(void *a)
{
	int16_t *reg = ((int16_t*)a);

	app_regs.REG_ANALOG_INPUTS[0] = reg[0];
	return true;
}


/************************************************************************/
/* REG_DI0                                                              */
/************************************************************************/
void app_read_REG_DI0(void)
{
	//app_regs.REG_DI0 = 0;

}

bool app_write_REG_DI0(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DI0 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED0                                                        */
/************************************************************************/
void app_read_REG_RESERVED0(void)
{
	//app_regs.REG_RESERVED0 = 0;

}

bool app_write_REG_RESERVED0(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED0 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED00                                                       */
/************************************************************************/
void app_read_REG_RESERVED00(void)
{
	//app_regs.REG_RESERVED00 = 0;

}

bool app_write_REG_RESERVED00(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED00 = reg;
	return true;
}


/************************************************************************/
/* REG_RANGE_AND_INPUT_FILTER                                           */
/************************************************************************/
void app_read_REG_RANGE_AND_INPUT_FILTER(void) {}
bool app_write_REG_RANGE_AND_INPUT_FILTER(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg & ~MSK_RANGE_AND_INPUT_FILTER)
		return false;
	
	PORTD.OUTCLR = 0x1C;
	PORTD.OUTSET = (reg << 2) & 0x1C;
	
	
	if (reg & 0x10)
		set_RANGE;
	else
		clr_RANGE;

	app_regs.REG_RANGE_AND_INPUT_FILTER = reg;
	return true;
}


/************************************************************************/
/* REG_SAMPLE_FREQUENCY                                                 */
/************************************************************************/
void app_read_REG_SAMPLE_FREQUENCY(void) {}
bool app_write_REG_SAMPLE_FREQUENCY(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg & ~MSK_SAMPLE_FREQUENCY)
		return false;

	app_regs.REG_SAMPLE_FREQUENCY = reg;
	return true;
}


/************************************************************************/
/* REG_DI0_CONF                                                         */
/************************************************************************/
void app_read_REG_DI0_CONF(void) {}
bool app_write_REG_DI0_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg & (~MSK_DI0_SEL))
		return false;

	app_regs.REG_DI0_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_DO0_CONF                                                         */
/************************************************************************/
void app_read_REG_DO0_CONF(void)
{
	//app_regs.REG_DO0_CONF = 0;

}

bool app_write_REG_DO0_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO0_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_DO0_PULSE                                                        */
/************************************************************************/
void app_read_REG_DO0_PULSE(void)
{
	//app_regs.REG_DO0_PULSE = 0;

}

bool app_write_REG_DO0_PULSE(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg < 1)
		return false;
		
	if (reg > 250)
		return false;

	app_regs.REG_DO0_PULSE = reg;
	return true;
}


/************************************************************************/
/* REG_DO_SET                                                           */
/************************************************************************/
void app_read_REG_DO_SET(void) {}
bool app_write_REG_DO_SET(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	//PORTA_OUTSET = reg & 0x0F;
	//if (reg & B_DO0)
	//	pulse_counter_ms = app_regs.REG_DO0_PULSE + 1;

	app_regs.REG_DO_WRITE = PORTA_OUT & 0x0F;
	app_regs.REG_DO_SET = reg;
	return true;
}


/************************************************************************/
/* REG_DO_CLEAR                                                         */
/************************************************************************/
void app_read_REG_DO_CLEAR(void) {}
bool app_write_REG_DO_CLEAR(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	//PORTA_OUTCLR = reg & 0x0F;

	app_regs.REG_DO_WRITE = PORTA_OUT & 0x0F;
	app_regs.REG_DO_CLEAR = reg;
	return true;
}


/************************************************************************/
/* REG_DO_TOGGLE                                                        */
/************************************************************************/
void app_read_REG_DO_TOGGLE(void) {}
bool app_write_REG_DO_TOGGLE(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	//if (!read_DO0)
	//	if (reg & B_DO0)
	//		pulse_counter_ms = app_regs.REG_DO0_PULSE + 1;
		
	//PORTA_OUTTGL = reg & 0x0F;
	
	app_regs.REG_DO_WRITE = PORTA_OUT & 0x0F;
	app_regs.REG_DO_TOGGLE = reg;
	return true;
}


/************************************************************************/
/* REG_DO_WRITE                                                         */
/************************************************************************/
void app_read_REG_DO_WRITE(void) {}
bool app_write_REG_DO_WRITE(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	//PORTA_OUT = (PORTA_OUT & (~0x0F)) | (reg & 0x0F);
	
	//if (reg & B_DO0)
	//	pulse_counter_ms = app_regs.REG_DO0_PULSE + 1;

	app_regs.REG_DO_WRITE = PORTA_OUT & 0x0F;
	return true;
}


/************************************************************************/
/* REG_RESERVED1                                                        */
/************************************************************************/
void app_read_REG_RESERVED1(void)
{
	//app_regs.REG_RESERVED1 = 0;

}

bool app_write_REG_RESERVED1(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED1 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED2                                                        */
/************************************************************************/
void app_read_REG_RESERVED2(void)
{
	//app_regs.REG_RESERVED2 = 0;

}

bool app_write_REG_RESERVED2(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED2 = reg;
	return true;
}


/************************************************************************/
/* REG_TRIGGER_DESTINY                                                  */
/************************************************************************/
void app_read_REG_TRIGGER_DESTINY(void) {}
bool app_write_REG_TRIGGER_DESTINY(void *a)
{
	uint8_t reg = *((uint8_t*)a);
	
	if (reg & (~MSK_TRIG_TO_DO0))
		return false;

	app_regs.REG_TRIGGER_DESTINY = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED3                                                        */
/************************************************************************/
void app_read_REG_RESERVED3(void)
{
	//app_regs.REG_RESERVED3 = 0;

}

bool app_write_REG_RESERVED3(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_RESERVED3 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED4                                                        */
/************************************************************************/
void app_read_REG_RESERVED4(void)
{
	//app_regs.REG_RESERVED4 = 0;

}

bool app_write_REG_RESERVED4(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_RESERVED4 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED5                                                        */
/************************************************************************/
void app_read_REG_RESERVED5(void)
{
	//app_regs.REG_RESERVED5 = 0;

}

bool app_write_REG_RESERVED5(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_RESERVED5 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED6                                                        */
/************************************************************************/
void app_read_REG_RESERVED6(void)
{
	//app_regs.REG_RESERVED6 = 0;

}

bool app_write_REG_RESERVED6(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_RESERVED6 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED7                                                        */
/************************************************************************/
void app_read_REG_RESERVED7(void)
{
	//app_regs.REG_RESERVED7 = 0;

}

bool app_write_REG_RESERVED7(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_RESERVED7 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED8                                                        */
/************************************************************************/
void app_read_REG_RESERVED8(void)
{
	//app_regs.REG_RESERVED8 = 0;

}

bool app_write_REG_RESERVED8(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_RESERVED8 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED9                                                        */
/************************************************************************/
void app_read_REG_RESERVED9(void)
{
	//app_regs.REG_RESERVED9 = 0;

}

bool app_write_REG_RESERVED9(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_RESERVED9 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED10                                                       */
/************************************************************************/
void app_read_REG_RESERVED10(void)
{
	//app_regs.REG_RESERVED10 = 0;

}

bool app_write_REG_RESERVED10(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED10 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED11                                                       */
/************************************************************************/
void app_read_REG_RESERVED11(void)
{
	//app_regs.REG_RESERVED11 = 0;

}

bool app_write_REG_RESERVED11(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED11 = reg;
	return true;
}


/************************************************************************/
/* REG_DO0_CH                                                           */
/************************************************************************/
void app_read_REG_DO0_CH(void)
{
	//app_regs.REG_DO0_CH = 0;

}

bool app_write_REG_DO0_CH(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO0_CH = reg;
	return true;
}


/************************************************************************/
/* REG_DO1_CH                                                           */
/************************************************************************/
void app_read_REG_DO1_CH(void)
{
	//app_regs.REG_DO1_CH = 0;

}

bool app_write_REG_DO1_CH(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO1_CH = reg;
	return true;
}


/************************************************************************/
/* REG_DO2_CH                                                           */
/************************************************************************/
void app_read_REG_DO2_CH(void)
{
	//app_regs.REG_DO2_CH = 0;

}

bool app_write_REG_DO2_CH(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO2_CH = reg;
	return true;
}


/************************************************************************/
/* REG_DO3_CH                                                           */
/************************************************************************/
void app_read_REG_DO3_CH(void)
{
	//app_regs.REG_DO3_CH = 0;

}

bool app_write_REG_DO3_CH(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO3_CH = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED12                                                       */
/************************************************************************/
void app_read_REG_RESERVED12(void)
{
	//app_regs.REG_RESERVED12 = 0;

}

bool app_write_REG_RESERVED12(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED12 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED13                                                       */
/************************************************************************/
void app_read_REG_RESERVED13(void)
{
	//app_regs.REG_RESERVED13 = 0;

}

bool app_write_REG_RESERVED13(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED13 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED14                                                       */
/************************************************************************/
void app_read_REG_RESERVED14(void)
{
	//app_regs.REG_RESERVED14 = 0;

}

bool app_write_REG_RESERVED14(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED14 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED15                                                       */
/************************************************************************/
void app_read_REG_RESERVED15(void)
{
	//app_regs.REG_RESERVED15 = 0;

}

bool app_write_REG_RESERVED15(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED15 = reg;
	return true;
}


/************************************************************************/
/* REG_DO0_TH_VALUE                                                     */
/************************************************************************/
void app_read_REG_DO0_TH_VALUE(void)
{
	//app_regs.REG_DO0_TH_VALUE = 0;

}

bool app_write_REG_DO0_TH_VALUE(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_DO0_TH_VALUE = reg;
	return true;
}


/************************************************************************/
/* REG_DO1_TH_VALUE                                                     */
/************************************************************************/
void app_read_REG_DO1_TH_VALUE(void)
{
	//app_regs.REG_DO1_TH_VALUE = 0;

}

bool app_write_REG_DO1_TH_VALUE(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_DO1_TH_VALUE = reg;
	return true;
}


/************************************************************************/
/* REG_DO2_TH_VALUE                                                     */
/************************************************************************/
void app_read_REG_DO2_TH_VALUE(void)
{
	//app_regs.REG_DO2_TH_VALUE = 0;

}

bool app_write_REG_DO2_TH_VALUE(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_DO2_TH_VALUE = reg;
	return true;
}


/************************************************************************/
/* REG_DO3_TH_VALUE                                                     */
/************************************************************************/
void app_read_REG_DO3_TH_VALUE(void)
{
	//app_regs.REG_DO3_TH_VALUE = 0;

}

bool app_write_REG_DO3_TH_VALUE(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_DO3_TH_VALUE = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED17                                                       */
/************************************************************************/
void app_read_REG_RESERVED17(void)
{
	//app_regs.REG_RESERVED17 = 0;

}

bool app_write_REG_RESERVED17(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_RESERVED17 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED18                                                       */
/************************************************************************/
void app_read_REG_RESERVED18(void)
{
	//app_regs.REG_RESERVED18 = 0;

}

bool app_write_REG_RESERVED18(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_RESERVED18 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED19                                                       */
/************************************************************************/
void app_read_REG_RESERVED19(void)
{
	//app_regs.REG_RESERVED19 = 0;

}

bool app_write_REG_RESERVED19(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_RESERVED19 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED20                                                       */
/************************************************************************/
void app_read_REG_RESERVED20(void)
{
	//app_regs.REG_RESERVED20 = 0;

}

bool app_write_REG_RESERVED20(void *a)
{
	int16_t reg = *((int16_t*)a);

	app_regs.REG_RESERVED20 = reg;
	return true;
}


/************************************************************************/
/* REG_DO0_TH_UP_SAMPLES                                                */
/************************************************************************/
void app_read_REG_DO0_TH_UP_SAMPLES(void)
{
	//app_regs.REG_DO0_TH_UP_SAMPLES = 0;

}

bool app_write_REG_DO0_TH_UP_SAMPLES(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO0_TH_UP_SAMPLES = reg;
	return true;
}


/************************************************************************/
/* REG_DO1_TH_UP_SAMPLES                                                */
/************************************************************************/
void app_read_REG_DO1_TH_UP_SAMPLES(void)
{
	//app_regs.REG_DO1_TH_UP_SAMPLES = 0;

}

bool app_write_REG_DO1_TH_UP_SAMPLES(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO1_TH_UP_SAMPLES = reg;
	return true;
}


/************************************************************************/
/* REG_DO2_TH_UP_SAMPLES                                                */
/************************************************************************/
void app_read_REG_DO2_TH_UP_SAMPLES(void)
{
	//app_regs.REG_DO2_TH_UP_SAMPLES = 0;

}

bool app_write_REG_DO2_TH_UP_SAMPLES(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO2_TH_UP_SAMPLES = reg;
	return true;
}


/************************************************************************/
/* REG_DO3_TH_UP_SAMPLES                                                */
/************************************************************************/
void app_read_REG_DO3_TH_UP_SAMPLES(void)
{
	//app_regs.REG_DO3_TH_UP_SAMPLES = 0;

}

bool app_write_REG_DO3_TH_UP_SAMPLES(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO3_TH_UP_SAMPLES = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED21                                                       */
/************************************************************************/
void app_read_REG_RESERVED21(void)
{
	//app_regs.REG_RESERVED21 = 0;

}

bool app_write_REG_RESERVED21(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_RESERVED21 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED22                                                       */
/************************************************************************/
void app_read_REG_RESERVED22(void)
{
	//app_regs.REG_RESERVED22 = 0;

}

bool app_write_REG_RESERVED22(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_RESERVED22 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED23                                                       */
/************************************************************************/
void app_read_REG_RESERVED23(void)
{
	//app_regs.REG_RESERVED23 = 0;

}

bool app_write_REG_RESERVED23(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_RESERVED23 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED24                                                       */
/************************************************************************/
void app_read_REG_RESERVED24(void)
{
	//app_regs.REG_RESERVED24 = 0;

}

bool app_write_REG_RESERVED24(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_RESERVED24 = reg;
	return true;
}


/************************************************************************/
/* REG_DO0_TH_DOWN_SAMPLES                                              */
/************************************************************************/
void app_read_REG_DO0_TH_DOWN_SAMPLES(void)
{
	//app_regs.REG_DO0_TH_DOWN_SAMPLES = 0;

}

bool app_write_REG_DO0_TH_DOWN_SAMPLES(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO0_TH_DOWN_SAMPLES = reg;
	return true;
}


/************************************************************************/
/* REG_DO1_TH_DOWN_SAMPLES                                              */
/************************************************************************/
void app_read_REG_DO1_TH_DOWN_SAMPLES(void)
{
	//app_regs.REG_DO1_TH_DOWN_SAMPLES = 0;

}

bool app_write_REG_DO1_TH_DOWN_SAMPLES(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO1_TH_DOWN_SAMPLES = reg;
	return true;
}


/************************************************************************/
/* REG_DO2_TH_DOWN_SAMPLES                                              */
/************************************************************************/
void app_read_REG_DO2_TH_DOWN_SAMPLES(void)
{
	//app_regs.REG_DO2_TH_DOWN_SAMPLES = 0;

}

bool app_write_REG_DO2_TH_DOWN_SAMPLES(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO2_TH_DOWN_SAMPLES = reg;
	return true;
}


/************************************************************************/
/* REG_DO3_TH_DOWN_SAMPLES                                              */
/************************************************************************/
void app_read_REG_DO3_TH_DOWN_SAMPLES(void)
{
	//app_regs.REG_DO3_TH_DOWN_SAMPLES = 0;

}

bool app_write_REG_DO3_TH_DOWN_SAMPLES(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_DO3_TH_DOWN_SAMPLES = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED25                                                       */
/************************************************************************/
void app_read_REG_RESERVED25(void)
{
	//app_regs.REG_RESERVED25 = 0;

}

bool app_write_REG_RESERVED25(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_RESERVED25 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED26                                                       */
/************************************************************************/
void app_read_REG_RESERVED26(void)
{
	//app_regs.REG_RESERVED26 = 0;

}

bool app_write_REG_RESERVED26(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_RESERVED26 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED27                                                       */
/************************************************************************/
void app_read_REG_RESERVED27(void)
{
	//app_regs.REG_RESERVED27 = 0;

}

bool app_write_REG_RESERVED27(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_RESERVED27 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED28                                                       */
/************************************************************************/
void app_read_REG_RESERVED28(void)
{
	//app_regs.REG_RESERVED28 = 0;

}

bool app_write_REG_RESERVED28(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_RESERVED28 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED29                                                       */
/************************************************************************/
void app_read_REG_RESERVED29(void)
{
	//app_regs.REG_RESERVED29 = 0;

}

bool app_write_REG_RESERVED29(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED29 = reg;
	return true;
}