# TimeCode
C# time code library based on SMPTE ST 12, with built-in support for all standard frame rates from 23.976 to 60 fps, including drop frame compensation modes where appropriate.

## Basic Usage

```c
//Define a frame rate
FrameRate frameRate = new FrameRate(FrameRates.FPS_29_97_DF);

//Construct from hours, minutes, seconds and frames
TimeCode timeCode1 = new TimeCode(23, 59, 59, 0, frameRate);

//Construct from a number of frames
TimeCode timeCode2 = new TimeCode(2589378, frameRate);

//Construct from time code string
TimeCode timeCode3 = TimeCode.FromString("23:59:59:00", frameRate);
```

## Calculations
```c
FrameRate frameRate = new FrameRate(FrameRates.FPS_29_97_DF);

TimeCode timeCode1 = new TimeCode(23, 59, 59, 0, frameRate);            
TimeCode timeCode2 = new TimeCode(60, frameRate);

//Result will roll forward to the next day: 00:00:01;00
TimeCode timeCode3 = timeCode1 + timeCode2;

//Result will roll backward to the previous day: 23:59:59;00
TimeCode timeCode4 = timeCode3 - timeCode2;
```
