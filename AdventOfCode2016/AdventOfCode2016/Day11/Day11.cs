namespace AdventOfCode2016.Day11;

public static class Day11
{
    // Get all microchips alone on a row except a single pair
    // Get all generators to the top row including the single pair
    // Get all microchips to the top row
    
    // valid input can only ever have rows containing all microchips, all generators or a mix where every microchip has its generator (there can be generators without microchips)

    public static int CountStepsTaken()
    {
/*
 	4										
	3										
	2			PO_M							PR_M								
L	1	PO_G			TH_G	TH_M	PR_G			RU_G	RU_M	CO_G	CO_M		
											
	4										
	3										
L	2			PO_M			TH_M			PR_M			RU_M						
	1	PO_G			TH_G			PR_G			RU_G			CO_G	CO_M	
				
1	
										
	4										
	3										
	2	
L	1	PO_G			TH_G			PR_G			RU_G	RU_M	CO_G	CO_M
		
2	
										
	4										
	3										
L	2			PO_M			TH_M			PR_M			RU_M			CO_M
	1	PO_G			TH_G			PR_G			RU_G			CO_G
		
3											

	4										
	3										
	2			PO_M			TH_M			PR_M			RU_M				
L	1	PO_G			TH_G			PR_G			RU_G			CO_G	CO_M	
	
4	
										
L	4	PO_G			TH_G								
	3										
	2			PO_M			TH_M			PR_M			RU_M						
	1									PR_G			RU_G			CO_G	CO_M	
						
7		
									
	4	PO_G											
	3										
	2			PO_M			TH_M			PR_M			RU_M						
L	1					TH_G			PR_G			RU_G			CO_G	CO_M	
				
10				
							
L	4	PO_G			TH_G			PR_G								
	3										
	2			PO_M			TH_M			PR_M			RU_M						
	1													RU_G			CO_G	CO_M		
						
13		
									
	4	PO_G			TH_G								
	3										
	2			PO_M			TH_M			PR_M			RU_M						
L	1									PR_G			RU_G			CO_G	CO_M
					
16	
										
L	4	PO_G			TH_G			PR_G			RU_G					
	3										
	2			PO_M			TH_M			PR_M			RU_M						
	1																	CO_G	CO_M		
				
19	
										
	4	PO_G			TH_G			PR_G					
	3										
	2			PO_M			TH_M			PR_M			RU_M						
L	1													RU_G			CO_G	CO_M
					
22		
									
L	4	PO_G			TH_G			PR_G			RU_G			CO_G		
	3										
	2			PO_M			TH_M			PR_M			RU_M						
	1																			CO_M	
						
25											

	4	PO_G			TH_G			PR_G			RU_G		
	3										
	2			PO_M			TH_M			PR_M			RU_M						
L	1																	CO_G	CO_M	
						
28	
										
L	4	PO_G			TH_G			PR_G			RU_G			CO_G	CO_M	
	3										
	2			PO_M			TH_M			PR_M			RU_M						
	1	
																							
31		
									
	4	PO_G			TH_G			PR_G			RU_G			CO_G		
	3										
L	2			PO_M			TH_M			PR_M			RU_M			CO_M			
	1		
						
33					
						
L	4	PO_G	PO_M	TH_G	TH_M	PR_G			RU_G			CO_G		
	3										
	2											PR_M			RU_M			CO_M			
	1	
									
35		
									
	4	PO_G	PO_M	TH_G			PR_G			RU_G			CO_G		
	3										
L	2							TH_M			PR_M			RU_M			CO_M			
	1									
	
37	
										
L	4	PO_G	PO_M	TH_G	TH_M	PR_G	PR_M	RU_G			CO_G		
	3										
	2															RU_M			CO_M			
	1			
								
39				
							
	4	PO_G	PO_M	TH_G	TH_M	PR_G			RU_G			CO_G		
	3										
L	2											PR_M			RU_M			CO_M			
	1	
																	
41		
									
L	4	PO_G	PO_M	TH_G	TH_M	PR_G	PR_M	RU_G	RU_M	CO_G		
	3										
	2																			CO_M			
	1	
											
43	
										
	4	PO_G	PO_M	TH_G	TH_M	PR_G	PR_M	RU_G			CO_G		
	3										
L	2															RU_M			CO_M			
	1		
							
45	
										
L	4	PO_G	PO_M	TH_G	TH_M	PR_G	PR_M	RU_G	RU_M	CO_G	CO_M	
	3										
	2																						
	1		
										
47		
									
 */

	    return 47;
    }
    
    public static int CountExtendedStepsTaken()
    {
/*
 	4										
	3										
	2			PO_M							PR_M								
L	1	PO_G			TH_G	TH_M	PR_G			RU_G	RU_M	CO_G	CO_M	EL_G	EL_M	DI_G	DI_M	
											
	4										
	3										
L	2			PO_M			TH_M			PR_M			RU_M						
	1	PO_G			TH_G			PR_G			RU_G			CO_G	CO_M	EL_G	EL_M	DI_G	DI_M
				
1	
										
	4										
	3										
	2	
L	1	PO_G			TH_G			PR_G			RU_G	RU_M	CO_G	CO_M	EL_G	EL_M	DI_G	DI_M
		
2	
										
	4										
	3										
L	2			PO_M			TH_M			PR_M			RU_M			CO_M
	1	PO_G			TH_G			PR_G			RU_G			CO_G			EL_G	EL_M	DI_G	DI_M
		
3											

	4										
	3										
	2			PO_M			TH_M			PR_M			RU_M				
L	1	PO_G			TH_G			PR_G			RU_G			CO_G	CO_M	EL_G	EL_M	DI_G	DI_M	
	
4	
			
	4										
	3										
L	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M
	1	PO_G			TH_G			PR_G			RU_G			CO_G			EL_G			DI_G	DI_M	
	
5	
			
	4										
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			
L	1	PO_G			TH_G			PR_G			RU_G			CO_G			EL_G	EL_M	DI_G	DI_M	
	
6

	4										
	3										
L	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M			DI_M
	1	PO_G			TH_G			PR_G			RU_G			CO_G			EL_G			DI_G		
	
7

	4										
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M			
L	1	PO_G			TH_G			PR_G			RU_G			CO_G			EL_G			DI_G	DI_M	
	
8
									
L	4	PO_G			TH_G								
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M							
	1									PR_G			RU_G			CO_G			EL_G			DI_G	DI_M
						
11		
									
	4	PO_G											
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M						
L	1					TH_G			PR_G			RU_G			CO_G			EL_G			DI_G	DI_M	
				
14				
							
L	4	PO_G			TH_G			PR_G								
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M					
	1													RU_G			CO_G			EL_G			DI_G	DI_M	
						
17		
									
	4	PO_G			TH_G								
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M					
L	1									PR_G			RU_G			CO_G			EL_G			DI_G	DI_M	
					
20	
										
L	4	PO_G			TH_G			PR_G			RU_G					
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M					
	1																	CO_G			EL_G			DI_G	DI_M	
				
23	
										
	4	PO_G			TH_G			PR_G					
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M					
L	1													RU_G			CO_G			EL_G			DI_G	DI_M	
				
26	
										
L	4	PO_G			TH_G			PR_G			RU_G			CO_G
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M					
	1																					EL_G			DI_G	DI_M	

29	
										
	4	PO_G			TH_G			PR_G			RU_G		
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M					
L	1																	CO_G			EL_G			DI_G	DI_M	

32
										
L	4	PO_G			TH_G			PR_G			RU_G			CO_G			EL_G
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M					
	1																									DI_G	DI_M	

35	
										
	4	PO_G			TH_G			PR_G			RU_G			CO_G
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M					
L	1																					EL_G			DI_G	DI_M	

38	

L	4	PO_G			TH_G			PR_G			RU_G			CO_G			EL_G			DI_G
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M					
	1																											DI_M	

41		
										
	4	PO_G			TH_G			PR_G			RU_G			CO_G			EL_G
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M					
L	1																									DI_G	DI_M	

44	

L	4	PO_G			TH_G			PR_G			RU_G			CO_G			EL_G			DI_G	DI_M
	3										
	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M					
	1																												

47		

	4	PO_G			TH_G			PR_G			RU_G			CO_G			EL_G			DI_G	
	3										
L	2			PO_M			TH_M			PR_M			RU_M			CO_M			EL_M			DI_M		
	1																												

49

L	4	PO_G	PO_M	TH_G	TH_M	PR_G			RU_G			CO_G			EL_G			DI_G	
	3										
	2											PR_M			RU_M			CO_M			EL_M			DI_M		
	1																												

51		
	
	4	PO_G	PO_M	TH_G			PR_G			RU_G			CO_G			EL_G			DI_G	
	3										
L	2							TH_M			PR_M			RU_M			CO_M			EL_M			DI_M		
	1																												

53
	
L	4	PO_G	PO_M	TH_G	TH_M	PR_G	PR_M	RU_G			CO_G			EL_G			DI_G	
	3										
	2															RU_M			CO_M			EL_M			DI_M		
	1																												

55		
	
	4	PO_G	PO_M	TH_G	TH_M	PR_G			RU_G			CO_G			EL_G			DI_G	
	3										
L	2											PR_M			RU_M			CO_M			EL_M			DI_M		
	1																												

57
	
L	4	PO_G	PO_M	TH_G	TH_M	PR_G	PR_M	RU_G	RU_M	CO_G			EL_G			DI_G	
	3										
	2																			CO_M			EL_M			DI_M		
	1																												

59		
	
	4	PO_G	PO_M	TH_G	TH_M	PR_G	PR_M	RU_G			CO_G			EL_G			DI_G	
	3										
L	2															RU_M			CO_M			EL_M			DI_M		
	1																												

61
	
L	4	PO_G	PO_M	TH_G	TH_M	PR_G	PR_M	RU_G	RU_M	CO_G	CO_M	EL_G			DI_G	
	3										
	2																							EL_M			DI_M		
	1																												

63		
	
	4	PO_G	PO_M	TH_G	TH_M	PR_G	PR_M	RU_G	RU_M	CO_G			EL_G			DI_G	
	3										
L	2																			CO_M			EL_M			DI_M		
	1																												

65
		
L	4	PO_G	PO_M	TH_G	TH_M	PR_G	PR_M	RU_G	RU_M	CO_G	CO_M	EL_G	EL_M	DI_G	
	3										
	2																											DI_M		
	1																												

67		
		
	4	PO_G	PO_M	TH_G	TH_M	PR_G	PR_M	RU_G	RU_M	CO_G	CO_M	EL_G			DI_G	
	3										
L	2																							EL_M			DI_M		
	1																												

69
			
L	4	PO_G	PO_M	TH_G	TH_M	PR_G	PR_M	RU_G	RU_M	CO_G	CO_M	EL_G	EL_M	DI_G	DI_M
	3										
	2																													
	1																												

71		
													
 */

	    return 71;
    }
}