#ifndef _APP_IOS_AND_REGS_H_
#define _APP_IOS_AND_REGS_H_
#include "cpu.h"

void init_ios(void);
/************************************************************************/
/* Definition of input pins                                             */
/************************************************************************/
// DI0                    Description: Input DIO
// MISO                   Description: Serial data output
// BUSY                   Description: Busy

#define read_DI0 read_io(PORTB, 0)              // DI0
#define read_MISO read_io(PORTC, 6)             // MISO
#define read_BUSY read_io(PORTD, 1)             // BUSY

/************************************************************************/
/* Definition of output pins                                            */
/************************************************************************/
// DO3                    Description: Output DO0
// DO2                    Description: Output DO1
// DO1                    Description: Output DO2
// DO0                    Description: Output DO3
// CS_ADC                 Description: Chip select
// CONVSTA                Description: Convert
// CONVSTB                Description: Convert
// MOSI                   Description: Serial data input
// SCK                    Description: Serial data clock
// RESET                  Description: Reset
// OS0                    Description: Oversampling bit 0
// OS1                    Description: Oversampling bit 1
// OS2                    Description: Oversampling bit 2
// RANGE                  Description: Selects range 5 or 10V

/* DO3 */
#define set_DO3 set_io(PORTB, 1)
#define clr_DO3 clear_io(PORTB, 1)
#define tgl_DO3 toggle_io(PORTB, 1)
#define read_DO3 read_io(PORTB, 1)

/* DO2 */
#define set_DO2 set_io(PORTA, 0)
#define clr_DO2 clear_io(PORTA, 0)
#define tgl_DO2 toggle_io(PORTA, 0)
#define read_DO2 read_io(PORTA, 0)

/* DO1 */
#define set_DO1 set_io(PORTA, 1)
#define clr_DO1 clear_io(PORTA, 1)
#define tgl_DO1 toggle_io(PORTA, 1)
#define read_DO1 read_io(PORTA, 1)

/* DO0 */
#define set_DO0 set_io(PORTA, 2)
#define clr_DO0 clear_io(PORTA, 2)
#define tgl_DO0 toggle_io(PORTA, 2)
#define read_DO0 read_io(PORTA, 2)

/* CS_ADC */
#define set_CS_ADC clear_io(PORTC, 4)
#define clr_CS_ADC set_io(PORTC, 4)
#define tgl_CS_ADC toggle_io(PORTC, 4)
#define read_CS_ADC read_io(PORTC, 4)

/* CONVSTA */
#define set_CONVSTA set_io(PORTC, 0)
#define clr_CONVSTA clear_io(PORTC, 0)
#define tgl_CONVSTA toggle_io(PORTC, 0)
#define read_CONVSTA read_io(PORTC, 0)

/* CONVSTB */
#define set_CONVSTB set_io(PORTC, 1)
#define clr_CONVSTB clear_io(PORTC, 1)
#define tgl_CONVSTB toggle_io(PORTC, 1)
#define read_CONVSTB read_io(PORTC, 1)

/* MOSI */
#define set_MOSI set_io(PORTC, 5)
#define clr_MOSI clear_io(PORTC, 5)
#define tgl_MOSI toggle_io(PORTC, 5)
#define read_MOSI read_io(PORTC, 5)

/* SCK */
#define set_SCK set_io(PORTC, 7)
#define clr_SCK clear_io(PORTC, 7)
#define tgl_SCK toggle_io(PORTC, 7)
#define read_SCK read_io(PORTC, 7)

/* RESET */
#define set_RESET set_io(PORTD, 0)
#define clr_RESET clear_io(PORTD, 0)
#define tgl_RESET toggle_io(PORTD, 0)
#define read_RESET read_io(PORTD, 0)

/* OS0 */
#define set_OS0 set_io(PORTD, 2)
#define clr_OS0 clear_io(PORTD, 2)
#define tgl_OS0 toggle_io(PORTD, 2)
#define read_OS0 read_io(PORTD, 2)

/* OS1 */
#define set_OS1 set_io(PORTD, 3)
#define clr_OS1 clear_io(PORTD, 3)
#define tgl_OS1 toggle_io(PORTD, 3)
#define read_OS1 read_io(PORTD, 3)

/* OS2 */
#define set_OS2 set_io(PORTD, 4)
#define clr_OS2 clear_io(PORTD, 4)
#define tgl_OS2 toggle_io(PORTD, 4)
#define read_OS2 read_io(PORTD, 4)

/* RANGE */
#define set_RANGE set_io(PORTA, 4)
#define clr_RANGE clear_io(PORTA, 4)
#define tgl_RANGE toggle_io(PORTA, 4)
#define read_RANGE read_io(PORTA, 4)


/************************************************************************/
/* Registers' structure                                                 */
/************************************************************************/
typedef struct
{
	uint8_t REG_START;
	int16_t REG_ANALOG_INPUTS[4];
	uint8_t REG_DI0;
	uint8_t REG_RESERVED0;
	uint8_t REG_RESERVED00;
	uint8_t REG_RANGE_AND_INPUT_FILTER;
	uint8_t REG_SAMPLE_FREQUENCY;
	uint8_t REG_DI0_CONF;
	uint8_t REG_DO0_CONF;
	uint8_t REG_DO0_PULSE;
	uint8_t REG_DO_SET;
	uint8_t REG_DO_CLEAR;
	uint8_t REG_DO_TOGGLE;
	uint8_t REG_DO_WRITE;
	uint8_t REG_RESERVED1;
	uint8_t REG_RESERVED2;
	uint8_t REG_TRIGGER_DESTINY;
	uint8_t REG_RESERVED3;
	uint8_t REG_RESERVED4;
	uint8_t REG_RESERVED5;
	uint8_t REG_RESERVED6;
	uint8_t REG_RESERVED7;
	uint8_t REG_RESERVED8;
	uint8_t REG_RESERVED9;
	uint8_t REG_RESERVED10;
	uint8_t REG_RESERVED11;
	uint8_t REG_DO0_CH;
	uint8_t REG_DO1_CH;
	uint8_t REG_DO2_CH;
	uint8_t REG_DO3_CH;
	uint8_t REG_RESERVED12;
	uint8_t REG_RESERVED13;
	uint8_t REG_RESERVED14;
	uint8_t REG_RESERVED15;
	int16_t REG_DO0_TH_VALUE;
	int16_t REG_DO1_TH_VALUE;
	int16_t REG_DO2_TH_VALUE;
	int16_t REG_DO3_TH_VALUE;
	uint8_t REG_RESERVED17;
	uint8_t REG_RESERVED18;
	uint8_t REG_RESERVED19;
	uint8_t REG_RESERVED20;
	uint16_t REG_DO0_TH_UP_SAMPLES;
	uint16_t REG_DO1_TH_UP_SAMPLES;
	uint16_t REG_DO2_TH_UP_SAMPLES;
	uint16_t REG_DO3_TH_UP_SAMPLES;
	uint8_t REG_RESERVED21;
	uint8_t REG_RESERVED22;
	uint8_t REG_RESERVED23;
	uint8_t REG_RESERVED24;
	uint16_t REG_DO0_TH_DOWN_SAMPLES;
	uint16_t REG_DO1_TH_DOWN_SAMPLES;
	uint16_t REG_DO2_TH_DOWN_SAMPLES;
	uint16_t REG_DO3_TH_DOWN_SAMPLES;
	uint8_t REG_RESERVED25;
	uint8_t REG_RESERVED26;
	uint8_t REG_RESERVED27;
	uint8_t REG_RESERVED28;
	uint8_t REG_RESERVED29;
} AppRegs;

/************************************************************************/
/* Registers' address                                                   */
/************************************************************************/
/* Registers */
#define ADD_REG_START                       32 // U8     Writing any value above ZERO will start the acquisition. ZERO will stop it.
#define ADD_REG_ANALOG_INPUTS               33 // I16    Value of the analog inputs
#define ADD_REG_DI0                         34 // U8     State of digital input 0 (DI0)
#define ADD_REG_RESERVED0                   35 // U8     Reserved for future purposes
#define ADD_REG_RESERVED00                  36 // U8     Contains the thresholds current status
#define ADD_REG_RANGE_AND_INPUT_FILTER      37 // U8     Configures voltage range and bandwidth
#define ADD_REG_SAMPLE_FREQUENCY            38 // U8     Sample rate of analog conversions
#define ADD_REG_DI0_CONF                    39 // U8     Configuration of the digital input 0 (DI0)
#define ADD_REG_DO0_CONF                    40 // U8     Configuration of the digital output 0 (DO0)
#define ADD_REG_DO0_PULSE                   41 // U8     Pulse for the digital output 0 (DO0) [1:255]
#define ADD_REG_DO_SET                      42 // U8     Set the digital outputs
#define ADD_REG_DO_CLEAR                    43 // U8     Clear the digital outputs
#define ADD_REG_DO_TOGGLE                   44 // U8     Toggle the digital outputs
#define ADD_REG_DO_WRITE                    45 // U8     Writes to the digital outputs
#define ADD_REG_RESERVED1                   46 // U8     Reserved for future purposes
#define ADD_REG_RESERVED2                   47 // U8     Reserved for future purposes
#define ADD_REG_TRIGGER_DESTINY             48 // U8     Configures where to send the acquisition trigger
#define ADD_REG_RESERVED3                   49 // U8     Reserved for future purposes
#define ADD_REG_RESERVED4                   50 // U8     Reserved for future purposes
#define ADD_REG_RESERVED5                   51 // U8     Reserved for future purposes
#define ADD_REG_RESERVED6                   52 // U8     Reserved for future purposes
#define ADD_REG_RESERVED7                   53 // U8     Reserved for future purposes
#define ADD_REG_RESERVED8                   54 // U8     Reserved for future purposes
#define ADD_REG_RESERVED9                   55 // U8     Reserved for future purposes
#define ADD_REG_RESERVED10                  56 // U8     Reserved for future purposes
#define ADD_REG_RESERVED11                  57 // U8     Reserved for future purposes
#define ADD_REG_DO0_CH                      58 // U8     Analog input channel used to feed filter of the threshold 0
#define ADD_REG_DO1_CH                      59 // U8     Analog input channel used to feed filter of the threshold 1
#define ADD_REG_DO2_CH                      60 // U8     Analog input channel used to feed filter of the threshold 2
#define ADD_REG_DO3_CH                      61 // U8     Analog input channel used to feed filter of the threshold 3
#define ADD_REG_RESERVED12                  62 // U8     
#define ADD_REG_RESERVED13                  63 // U8     
#define ADD_REG_RESERVED14                  64 // U8     
#define ADD_REG_RESERVED15                  65 // U8     
#define ADD_REG_DO0_TH_VALUE                66 // I16    Value to be compared
#define ADD_REG_DO1_TH_VALUE                67 // I16    
#define ADD_REG_DO2_TH_VALUE                68 // I16    
#define ADD_REG_DO3_TH_VALUE                69 // I16    
#define ADD_REG_RESERVED17                  70 // U8     
#define ADD_REG_RESERVED18                  71 // U8     
#define ADD_REG_RESERVED19                  72 // U8     
#define ADD_REG_RESERVED20                  73 // U8     
#define ADD_REG_DO0_TH_UP_SAMPLES           74 // U16    Number of samples above the configured threshold to set the digital output
#define ADD_REG_DO1_TH_UP_SAMPLES           75 // U16    
#define ADD_REG_DO2_TH_UP_SAMPLES           76 // U16    
#define ADD_REG_DO3_TH_UP_SAMPLES           77 // U16    
#define ADD_REG_RESERVED21                  78 // U8     
#define ADD_REG_RESERVED22                  79 // U8     
#define ADD_REG_RESERVED23                  80 // U8     
#define ADD_REG_RESERVED24                  81 // U8     
#define ADD_REG_DO0_TH_DOWN_SAMPLES         82 // U16    Number of samples bellow the configured threshold to set the digital output
#define ADD_REG_DO1_TH_DOWN_SAMPLES         83 // U16    
#define ADD_REG_DO2_TH_DOWN_SAMPLES         84 // U16    
#define ADD_REG_DO3_TH_DOWN_SAMPLES         85 // U16    
#define ADD_REG_RESERVED25                  86 // U8     
#define ADD_REG_RESERVED26                  87 // U8     
#define ADD_REG_RESERVED27                  88 // U8     
#define ADD_REG_RESERVED28                  89 // U8     
#define ADD_REG_RESERVED29                  90 // U8     

/************************************************************************/
/* PWM Generator registers' memory limits                               */
/*                                                                      */
/* DON'T change the APP_REGS_ADD_MIN value !!!                          */
/* DON'T change these names !!!                                         */
/************************************************************************/
/* Memory limits */
#define APP_REGS_ADD_MIN                    0x20
#define APP_REGS_ADD_MAX                    0x5A
#define APP_NBYTES_OF_REG_BANK              78

/************************************************************************/
/* Registers' bits                                                      */
/************************************************************************/
#define B_START                            (1<<0)       // 
#define B_DI0                              (1<<0)       // 
#define MSK_RANGE_AND_INPUT_FILTER         0x17         // 
#define GM_5V_1K5                          0x06         // 
#define GM_5V_3K                           0x05         // 
#define GM_5V_6K                           0x04         // 
#define GM_5V_10K3                         0x03         // 
#define GM_5V_13K7                         0x02         // 
#define GM_5V_15K                          0x01         // 
#define GM_10V_1K5                         0x16         // 
#define GM_10V_3K                          0x15         // 
#define GM_10V_6K                          0x14         // 
#define GM_10V_11K9                        0x13         // 
#define GM_10V_18K5                        0x12         // 
#define GM_10V_22K                         0x11         // 
#define MSK_SAMPLE_FREQUENCY               (3<<0)       // 
#define GM_1KHZ                            0x00         // 
#define GM_2KHZ                            0x01         // 
#define MSK_DI0_SEL                        (7<<0)       // 
#define GM_DI0_SYNC                        (0<<0)       // Use as a pure digital input
#define GM_DI0_RISE_START_ACQ              (1<<0)       // Start acquisition when rising edge and stop when falling edge
#define GM_DI0_FALL_START_ACQ              (2<<0)       // Start acquisition when falling edge and stop when rising edge
#define GM_DI0_RISE_CATCH_SAMPLE           (3<<0)       // Acquire a sample when a rising edge is detected
#define GM_DI0_FALL_CATCH_SAMPLE           (4<<0)       // Acquire a sample when a falling edge is detected
#define MSK_DO0_SEL                        (3<<0)       // 
#define GM_DO0_DIG                         (0<<0)       // Use as a pure digital output like all the other digital outputs
#define GM_DO0_TGL_EACH_SEC                (1<<0)       // Toggle each second when acquiring
#define GM_DO0_PULSE                       (2<<0)       // The digital output will be ONE during period specified by register DO0_PULSE
#define B_DO0                              (1<<0)       // 
#define B_DO1                              (1<<1)       // 
#define B_DO2                              (1<<2)       // 
#define B_DO3                              (1<<3)       // 
#define B_DO0_CHANGED                      (1<<4)       // 
#define B_DO1_CHANGED                      (1<<5)       // 
#define B_DO2_CHANGED                      (1<<6)       // 
#define B_DO3_CHANGED                      (1<<7)       // 
#define MSK_TRIG_TO_DO0                    0x07         // 
#define GM_TRIG_TO_NONE                    (0<<0)       // 
#define GM_TRIG_TO_DO0                     (1<<0)       // 
#define GM_TRIG_TO_DO1                     (2<<0)       // 
#define GM_TRIG_TO_DO2                     (3<<0)       // 
#define GM_TRIG_TO_DO3                     (4<<0)       // 
#define MSK_ANA_CH                         (16<<0)      // 
#define GM_ANA0                            (0<<0)       // Analog input channel 0
#define GM_ANA1                            (1<<0)       // Analog input channel 1
#define GM_ANA2                            (2<<0)       // Analog input channel 2
#define GM_ANA3                            (3<<0)       // Analog input channel 3
#define GM_NOT_USED                        (8<<0)       // Threshold not used

#endif /* _APP_REGS_H_ */