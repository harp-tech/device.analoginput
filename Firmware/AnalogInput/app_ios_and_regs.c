#include <avr/io.h>
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"

/************************************************************************/
/* Configure and initialize IOs                                         */
/************************************************************************/
void init_ios(void)
{	/* Configure input pins */
	io_pin2in(&PORTB, 0, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // DI0
	io_pin2in(&PORTC, 6, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // MISO
	io_pin2in(&PORTD, 1, PULL_IO_UP, SENSE_IO_EDGE_FALLING);             // BUSY

	/* Configure input interrupts */
	io_set_int(&PORTB, INT_LEVEL_LOW, 0, (1<<0), false);                 // DI0
	io_set_int(&PORTD, INT_LEVEL_LOW, 0, (1<<1), false);                 // BUSY

	/* Configure output pins */
	io_pin2out(&PORTB, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO3
	io_pin2out(&PORTA, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO2
	io_pin2out(&PORTA, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO1
	io_pin2out(&PORTA, 2, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO0
	io_pin2out(&PORTC, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CONVSTA
	io_pin2out(&PORTC, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CONVSTB
	io_pin2out(&PORTC, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // CS_ADC
	io_pin2out(&PORTC, 5, OUT_IO_DIGITAL, IN_EN_IO_DIS);                 // MOSI
	io_pin2out(&PORTC, 7, OUT_IO_DIGITAL, IN_EN_IO_DIS);                 // SCK
	io_pin2out(&PORTD, 0, OUT_IO_WIREDAND, IN_EN_IO_EN);                 // RESET
	io_pin2out(&PORTD, 2, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // OS0
	io_pin2out(&PORTD, 3, OUT_IO_DIGITAL, IN_EN_IO_DIS);                 // OS1
	io_pin2out(&PORTD, 4, OUT_IO_DIGITAL, IN_EN_IO_DIS);                 // OS2
	io_pin2out(&PORTA, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // RANGE

	/* Initialize output pins */
	clr_DO3;
	clr_DO2;
	clr_DO1;
	clr_DO0;
	set_CS_ADC;
	clr_CONVST;
	clr_CONVSTB;
	clr_MOSI;
	clr_SCK;
	clr_RESET;
	clr_OS0;
	set_OS1;
	set_OS2;
	clr_RANGE;
}

/************************************************************************/
/* Registers' stuff                                                     */
/************************************************************************/
AppRegs app_regs;

uint8_t app_regs_type[] = {
	TYPE_U8,
	TYPE_I16,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_I16,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8
};

uint16_t app_regs_n_elements[] = {
	1,
	4,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1
};

uint8_t *app_regs_pointer[] = {
	(uint8_t*)(&app_regs.REG_START),
	(uint8_t*)(app_regs.REG_ANALOG_INPUTS),
	(uint8_t*)(&app_regs.REG_DI0),
	(uint8_t*)(&app_regs.REG_RESERVED0),
	(uint8_t*)(&app_regs.REG_RESERVED00),
	(uint8_t*)(&app_regs.REG_RANGE_AND_INPUT_FILTER),
	(uint8_t*)(&app_regs.REG_SAMPLE_FREQUENCY),
	(uint8_t*)(&app_regs.REG_DI0_CONF),
	(uint8_t*)(&app_regs.REG_DO0_CONF),
	(uint8_t*)(&app_regs.REG_DO0_PULSE),
	(uint8_t*)(&app_regs.REG_DO_SET),
	(uint8_t*)(&app_regs.REG_DO_CLEAR),
	(uint8_t*)(&app_regs.REG_DO_TOGGLE),
	(uint8_t*)(&app_regs.REG_DO_WRITE),
	(uint8_t*)(&app_regs.REG_RESERVED1),
	(uint8_t*)(&app_regs.REG_RESERVED2),
	(uint8_t*)(&app_regs.REG_TRIGGER_DESTINY),
	(uint8_t*)(&app_regs.REG_RESERVED3),
	(uint8_t*)(&app_regs.REG_RESERVED4),
	(uint8_t*)(&app_regs.REG_RESERVED5),
	(uint8_t*)(&app_regs.REG_RESERVED6),
	(uint8_t*)(&app_regs.REG_RESERVED7),
	(uint8_t*)(&app_regs.REG_RESERVED8),
	(uint8_t*)(&app_regs.REG_RESERVED9),
	(uint8_t*)(&app_regs.REG_RESERVED10),
	(uint8_t*)(&app_regs.REG_RESERVED11),
	(uint8_t*)(&app_regs.REG_DO0_CH),
	(uint8_t*)(&app_regs.REG_DO1_CH),
	(uint8_t*)(&app_regs.REG_DO2_CH),
	(uint8_t*)(&app_regs.REG_DO3_CH),
	(uint8_t*)(&app_regs.REG_RESERVED12),
	(uint8_t*)(&app_regs.REG_RESERVED13),
	(uint8_t*)(&app_regs.REG_RESERVED14),
	(uint8_t*)(&app_regs.REG_RESERVED15),
	(uint8_t*)(&app_regs.REG_DO0_TH_VALUE),
	(uint8_t*)(&app_regs.REG_DO1_TH_VALUE),
	(uint8_t*)(&app_regs.REG_DO2_TH_VALUE),
	(uint8_t*)(&app_regs.REG_DO3_TH_VALUE),
	(uint8_t*)(&app_regs.REG_RESERVED17),
	(uint8_t*)(&app_regs.REG_RESERVED18),
	(uint8_t*)(&app_regs.REG_RESERVED19),
	(uint8_t*)(&app_regs.REG_RESERVED20),
	(uint8_t*)(&app_regs.REG_DO0_TH_UP_SAMPLES),
	(uint8_t*)(&app_regs.REG_DO1_TH_UP_SAMPLES),
	(uint8_t*)(&app_regs.REG_DO2_TH_UP_SAMPLES),
	(uint8_t*)(&app_regs.REG_DO3_TH_UP_SAMPLES),
	(uint8_t*)(&app_regs.REG_RESERVED21),
	(uint8_t*)(&app_regs.REG_RESERVED22),
	(uint8_t*)(&app_regs.REG_RESERVED23),
	(uint8_t*)(&app_regs.REG_RESERVED24),
	(uint8_t*)(&app_regs.REG_DO0_TH_DOWN_SAMPLES),
	(uint8_t*)(&app_regs.REG_DO1_TH_DOWN_SAMPLES),
	(uint8_t*)(&app_regs.REG_DO2_TH_DOWN_SAMPLES),
	(uint8_t*)(&app_regs.REG_DO3_TH_DOWN_SAMPLES),
	(uint8_t*)(&app_regs.REG_RESERVED25),
	(uint8_t*)(&app_regs.REG_RESERVED26),
	(uint8_t*)(&app_regs.REG_RESERVED27),
	(uint8_t*)(&app_regs.REG_RESERVED28),
	(uint8_t*)(&app_regs.REG_RESERVED29)
};