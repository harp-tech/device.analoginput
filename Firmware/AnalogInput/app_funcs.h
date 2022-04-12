#ifndef _APP_FUNCTIONS_H_
#define _APP_FUNCTIONS_H_
#include <avr/io.h>


/************************************************************************/
/* Define if not defined                                                */
/************************************************************************/
#ifndef bool
	#define bool uint8_t
#endif
#ifndef true
	#define true 1
#endif
#ifndef false
	#define false 0
#endif


/************************************************************************/
/* Prototypes                                                           */
/************************************************************************/
void app_read_REG_START(void);
void app_read_REG_ANALOG_INPUTS(void);
void app_read_REG_DI0(void);
void app_read_REG_RESERVED0(void);
void app_read_REG_RESERVED00(void);
void app_read_REG_RANGE_AND_INPUT_FILTER(void);
void app_read_REG_SAMPLE_FREQUENCY(void);
void app_read_REG_DI0_CONF(void);
void app_read_REG_DO0_CONF(void);
void app_read_REG_DO0_PULSE(void);
void app_read_REG_DO_SET(void);
void app_read_REG_DO_CLEAR(void);
void app_read_REG_DO_TOGGLE(void);
void app_read_REG_DO_WRITE(void);
void app_read_REG_RESERVED1(void);
void app_read_REG_RESERVED2(void);
void app_read_REG_TRIGGER_DESTINY(void);
void app_read_REG_RESERVED3(void);
void app_read_REG_RESERVED4(void);
void app_read_REG_RESERVED5(void);
void app_read_REG_RESERVED6(void);
void app_read_REG_RESERVED7(void);
void app_read_REG_RESERVED8(void);
void app_read_REG_RESERVED9(void);
void app_read_REG_RESERVED10(void);
void app_read_REG_RESERVED11(void);
void app_read_REG_DO0_CH(void);
void app_read_REG_DO1_CH(void);
void app_read_REG_DO2_CH(void);
void app_read_REG_DO3_CH(void);
void app_read_REG_RESERVED12(void);
void app_read_REG_RESERVED13(void);
void app_read_REG_RESERVED14(void);
void app_read_REG_RESERVED15(void);
void app_read_REG_DO0_TH_VALUE(void);
void app_read_REG_DO1_TH_VALUE(void);
void app_read_REG_DO2_TH_VALUE(void);
void app_read_REG_DO3_TH_VALUE(void);
void app_read_REG_RESERVED17(void);
void app_read_REG_RESERVED18(void);
void app_read_REG_RESERVED19(void);
void app_read_REG_RESERVED20(void);
void app_read_REG_DO0_TH_UP_SAMPLES(void);
void app_read_REG_DO1_TH_UP_SAMPLES(void);
void app_read_REG_DO2_TH_UP_SAMPLES(void);
void app_read_REG_DO3_TH_UP_SAMPLES(void);
void app_read_REG_RESERVED21(void);
void app_read_REG_RESERVED22(void);
void app_read_REG_RESERVED23(void);
void app_read_REG_RESERVED24(void);
void app_read_REG_DO0_TH_DOWN_SAMPLES(void);
void app_read_REG_DO1_TH_DOWN_SAMPLES(void);
void app_read_REG_DO2_TH_DOWN_SAMPLES(void);
void app_read_REG_DO3_TH_DOWN_SAMPLES(void);
void app_read_REG_RESERVED25(void);
void app_read_REG_RESERVED26(void);
void app_read_REG_RESERVED27(void);
void app_read_REG_RESERVED28(void);
void app_read_REG_RESERVED29(void);

bool app_write_REG_START(void *a);
bool app_write_REG_ANALOG_INPUTS(void *a);
bool app_write_REG_DI0(void *a);
bool app_write_REG_RESERVED0(void *a);
bool app_write_REG_RESERVED00(void *a);
bool app_write_REG_RANGE_AND_INPUT_FILTER(void *a);
bool app_write_REG_SAMPLE_FREQUENCY(void *a);
bool app_write_REG_DI0_CONF(void *a);
bool app_write_REG_DO0_CONF(void *a);
bool app_write_REG_DO0_PULSE(void *a);
bool app_write_REG_DO_SET(void *a);
bool app_write_REG_DO_CLEAR(void *a);
bool app_write_REG_DO_TOGGLE(void *a);
bool app_write_REG_DO_WRITE(void *a);
bool app_write_REG_RESERVED1(void *a);
bool app_write_REG_RESERVED2(void *a);
bool app_write_REG_TRIGGER_DESTINY(void *a);
bool app_write_REG_RESERVED3(void *a);
bool app_write_REG_RESERVED4(void *a);
bool app_write_REG_RESERVED5(void *a);
bool app_write_REG_RESERVED6(void *a);
bool app_write_REG_RESERVED7(void *a);
bool app_write_REG_RESERVED8(void *a);
bool app_write_REG_RESERVED9(void *a);
bool app_write_REG_RESERVED10(void *a);
bool app_write_REG_RESERVED11(void *a);
bool app_write_REG_DO0_CH(void *a);
bool app_write_REG_DO1_CH(void *a);
bool app_write_REG_DO2_CH(void *a);
bool app_write_REG_DO3_CH(void *a);
bool app_write_REG_RESERVED12(void *a);
bool app_write_REG_RESERVED13(void *a);
bool app_write_REG_RESERVED14(void *a);
bool app_write_REG_RESERVED15(void *a);
bool app_write_REG_DO0_TH_VALUE(void *a);
bool app_write_REG_DO1_TH_VALUE(void *a);
bool app_write_REG_DO2_TH_VALUE(void *a);
bool app_write_REG_DO3_TH_VALUE(void *a);
bool app_write_REG_RESERVED17(void *a);
bool app_write_REG_RESERVED18(void *a);
bool app_write_REG_RESERVED19(void *a);
bool app_write_REG_RESERVED20(void *a);
bool app_write_REG_DO0_TH_UP_SAMPLES(void *a);
bool app_write_REG_DO1_TH_UP_SAMPLES(void *a);
bool app_write_REG_DO2_TH_UP_SAMPLES(void *a);
bool app_write_REG_DO3_TH_UP_SAMPLES(void *a);
bool app_write_REG_RESERVED21(void *a);
bool app_write_REG_RESERVED22(void *a);
bool app_write_REG_RESERVED23(void *a);
bool app_write_REG_RESERVED24(void *a);
bool app_write_REG_DO0_TH_DOWN_SAMPLES(void *a);
bool app_write_REG_DO1_TH_DOWN_SAMPLES(void *a);
bool app_write_REG_DO2_TH_DOWN_SAMPLES(void *a);
bool app_write_REG_DO3_TH_DOWN_SAMPLES(void *a);
bool app_write_REG_RESERVED25(void *a);
bool app_write_REG_RESERVED26(void *a);
bool app_write_REG_RESERVED27(void *a);
bool app_write_REG_RESERVED28(void *a);
bool app_write_REG_RESERVED29(void *a);


#endif /* _APP_FUNCTIONS_H_ */