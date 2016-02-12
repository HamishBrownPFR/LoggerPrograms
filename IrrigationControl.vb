'CR1000
'Created by Short Cut (3.0)

'Declare Variables and Units
Dim LCount
Dim NowMin						
Dim CellCon

Public BattV
Public PTemp_C
Public T107_C
Public LeafWet_kohms
Public RTime(9)	

Units BattV=Volts
Units PTemp_C=Deg C
Units T107_C=Deg C
Units LeafWet_kohms=kilohms						

'Define Data Tables
DataTable(PotTables,True,-1)
	DataInterval(0,30,min,10)
	Average(1,BattV,FP2,False)					
	Average(1,PTemp_C,FP2,False)					
	Average(1,T107_C,FP2)
	Average(1,LeafWet_kohms,FP2,False)	
EndTable

'Main Program
BeginProg
	'Main Scan
	Scan(30,sec,1,0)
		'Default Datalogger Battery Voltage measurement 'BattV'
		Battery(BattV)
		'Default Wiring Panel Temperature measurement 'PTemp_C'
		PanelTemp(PTemp_C,_60Hz)
		'109 Temperature Probe measurement 'T109_C'
		'Measure thermoster temperature Therm109 (Dest, Reps, SEChan, VX/ExChan, SettlingTime, Integ, Mult, Offset)		
		Therm107(T107_C,1,8,3,0,_60Hz,1,0)
		'237 Leaf Wetness Sensor measurement 'LeafWet_kohms'.  BrHalf(Dest, Reps, Range, SEChan, VX/ExChan, MeasPEx, ExmV, RevEx, Settlingtime, Integ, Mult, Offset)				
		BrHalf(LeafWet_kohms,1,mV25,15,1,1,2500,True,0,250,1,0)				
		LeafWet_kohms=(1/LeafWet_kohms)-101				
		'Call Data Tables and Store Data
		CallTable(Table1)
	
		Delay(0,150,mSec)  'Delay(Option, Delay, Units)				
		'Cellular Phone Control				
		'Turn the cell phone on between 900 and1000 hours				
		'RealTime(RTime(1))		
		'		'1=year, 2=month, 3=day of month, 4=hour of day, 5=minutes, 6=seconds, 7=microseconds, 8=day of week, 9=day of year		
		'		NowMin=RTime(4)*60+RTime(5)		
		'		If NowMin>= 540 and NowMIn<=600 Then		
		'			CellCon=1	
		'		Else		
		'			CellCon=0	
		'		EndIf		
		'		If BattV<11.5 then		
		'			CellCon=0	
		'		EndIf		
		'PortSet(7,CellCon)				
		PortSet(1,1)				
		Delay(0,2,Sec)
		PortSet(1,0)
		PortSet(2,1)				
		Delay(0,2,Sec)
		PortSet(2,0)
		Delay(0,150,mSec)  'Delay(Option, Delay, Units)				
		'Call Data Tables and Store Data
		CallTable(PotTables)
	NextScan
EndProg
