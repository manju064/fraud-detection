# fraud-detection

# Codingassignment assignment
This component identifies if someone is using different names to trick the system into believing they are different people! 
Api end points for below functional usages
- Receives and stores a person 
- Calculates the probability that 2 persons are the same physical person. 

A person is represented by the following attributes: 
- First name 
- Last name 
- Date of birth (can be unknown) 
- Identification number (can be unknown) 

The matching logic should work in the following way: 
- If the Identification number matches then 100% 
- Otherwise: 
	If the last name is the same +40% 
	If the first name is the same +20% 
	If the first name is similar +15% (see examples) 
	If the date of birth matches + 40% 
	If the dates of birth are known and not the same, there is no match 
	
Similar first names examples: 
- Andrew and A. (initials) 
- Andrew and Andew (typo) 
- Andrew and Andy (diminutive) 