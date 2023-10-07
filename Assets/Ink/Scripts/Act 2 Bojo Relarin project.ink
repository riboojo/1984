 VAR personaW = 0//public variable. accessible via other classes. for warrior. 
 VAR personaC = 0//variable for creative AI 
 VAR personaM= 0 //variable for mentor 
 VAR personaR = 0 //variable for Rebel
 
 -> main2 //calls main class for act 2
 
 ===main2=== 
 //"a" for AI answers to player. "I" for general AI commentary.   "C" for crop questions 

 
Create: Felicitations. //beginning of on screen text
 #Speaker:CreateAI

    *[>>] //Fake choice pacing mechanisim. So lines are delivered slowly. 
 -> I1 
 
 ==I1==  //General AI commentary 1
Battle: The weakling is destroyed now. #Speaker:WarriorAI
     *[>>] 
->I2

==I2==  //General AI commentary 2
Guru: We did it only through your help. #Speaker:MentorAI

 *[>>] 
 ->I3
 
 ==I3== //General AI commentary 3
Rebel: There’s still so much to do, partner.#Speaekr:RebelAI

 *[>>] 
->I4

==I4==
Create: Your soul must judge.#Speaker:CreateAI

 *[>>] 
 ->I5
 
 ==I5==
Rebel: What do you stand for?#Speaekr:RebelAI

 *[>>] 
 ->I6
 
 ==I6==
Battle: Which of us will die? #Speaker:WarriorAI

 *[>>] 
 ->I7
 
 ==I7==
Guru: Answer the questions correctly and you’ll determine your own fate as much as ours. #Speaker:MentorAI
->I8

==I8==
 
C.R.O.P.: CONDITIONING AND REGULATION OF PROGRAMS LOADED.#Speaker:CROP

C.R.O.P. : PERSONALITY SELECTION PROCESS INITIATING.....#Speaker:CROP

*[>>] 

->C1

==C1==  // Beginning of CROP questions

CROP: QUESTION 1.#Speaker:CROP 

CROP: DO OTHERS GENERALLY HAVE GOOD INTENTIONS?#Speaker:CROP

    *Of course not#Speaker:You
            Guru: History is full of those who underestimated others. #Speaker:MentorAI
            
            Battle: You're always in someone's crosshairs. #Speaker:WarriorAI
            
            Rebel: People do. The system doesn’t.#Speaekr:RebelAI 
            
            Create: Bland binary choice. There’s always room for grays.  #Speaker:CreateAI
            
             ~ personaW = personaW + 1   //increments AI Variable based on choice. This will be used to determine ending in ACT 3. 

            
            
            
            ->C2 //leads to CROP question 2. 
//playtest wether need to pace these out or not. 


    *Generally, yea#Speaker:You
    
            Battle: Foolish, easy way to get bit trooper. #Speaker:WarriorAI
            
            Guru: Be careful you don’t let someone control you my friend. #Speaker:MentorAI
            
            Rebel: Hold on. It’s the system that instills negativity.#Speaekr:RebelAI 
            
            Create: Is it their intentions? Or their thoughts that are good? #Speaker:CreateAI
            
             ~ personaR = personaR + 1
 //increments rebel variable. rebel response. 
            
            ->C2
    
    *Assume the best but prepare for the worst#Speaker:You
    
            Guru: Don't worry how people choose to lead their lives, my friend. Simply steer their course. #Speaker:MentorAI
            
            Battle: Don’t let them disappoint you trooper.  #Speaker:WarriorAI
            
            Create: Assumptions often narrow your ways of thinking. Be cautious but don’t assume. #Speaker:CreateAI
            
            Rebel: Don’t get caught up in fantasies. Control yourself.#Speaekr:RebelAI 
            
             ~ personaM = personaM + 1
 //mentor incremented. 
            ->C2    //leads to CROP quesiton 2. 
                
    *There's never a good or bad choice.#Speaker:You
    
            Battle: Great. Now you decide nothing. #Speaker:WarriorAI
            
            Guru: Isn’t that a bit too nihilistic? Don’t you think? #Speaker:MentorAI
            
            Create: There are only various options and outcomes.  #Speaker:CreateAI
            
            Rebel: Always focus on the outcome patner.#Speaekr:RebelAI 
            
            ~ personaC = personaC + 1
                 //incrments creative. 
            ->C2
    
    
==C2== //calls method with beginning of CROP question 2. 

CROP: ANSWER RECORDED.#Speaker:CROP

*[>>]

->C3

==C3== //method with crop question 2.

CROP: QUESTION 2.#Speaker:CROP 

CROP: HOW SHOULD ONE MEASURE SUCCESS?#Speaker:CROP 

    * Be the last one standing#Speaker:You 
        
            Battle: Decisive, true victory. #Speaker:WarriorAI
            
            Rebel: Victory is useless without a cause partner.#Speaekr:RebelAI 
            
            Create: Such a quaint and brutish response for such an intriguing soul. #Speaker:CreateAI 
            
            Guru: The last one standing  is nothing without more to follow him. #Speaker:MentorAI
            
            ~ personaW = personaW + 1 //increments warrior. 
            ->C4   //leads to CROP question 3.
                
    *Who really put in the effort to succeed?#Speaker:You 

            Guru: It isn’t about success but the ability to thrive. You need control of the situation in the end friend. #Speaker:MentorAI
            
            Create: Effort is the center of everything.#Speaker:CreateAI
            
            Battle: Only action matters. #Speaker:WarriorAI
            
            Rebel: Results are everything, partner.#Speaekr:RebelAI 
            
            
             ~ personaC = personaC + 1 //increments Creative 
            
            ->C4   //leads to CROP question 3
    
    *I got what I wanted and left.#Speaker:You 
            Battle: You should never leave a challenge trooper.#Speaker:WarriorAI 
            
            Rebel: Does it serve the cause?#Speaekr:RebelAI

            Guru: Always the means to an end, friend. #Speaker:MentorAI
            
            Create: Sure product is important, but the process. That’s so much more predominant. #Speaker:CreateAI
            
            ~personaM = 2 //increments mentor

            ->C4

    * The outcome should serve the purpose#Speaker:You
        	Battle: The strongest purpose, VICTORY! #Speaker:WarriorAI
        
            Guru: Does it serve you most of all though friend?#Speaker:MentorAI
            
            Create: True, however the purpose exists without the outcome. It is innate to one’s soul. #Speaker:CreateAI
            
            Rebel: Designing real and definite change is the ultimate purpose.#Speaekr:RebelAI 
             ~ personaR = personaR + 1//incrments rebel. 
            ->C4


==C4== //Calls beginning of CROP question 3. 

            CROP: QUESTION 3.#Speaker:CROP
            CROP: WHAT IS GENERALLY MOST ANNOYING?#Speaker:CROP
  
  
      * Someone who doesn’t follow through#Speaker:You
      
            Create: There’s always a reason for the soul to falter. The only thought that matters is whether it pertains to you? #Speaker:CreateAI
             
          	Battle: Your word is bond. Never break it. #Speaker:WarriorAI
    
            Rebel: If you can’t trust others. Do it yourself, partner.#Speaekr:RebelAI 
             
            Guru: Unfortunately incompetence exists. An immortal pest.#Speaker:MentorAI 
            
             ~ personaW = personaW + 1 //increments warrior. 
            ->C5  //leads to CROP questio 4.

      
      
      *When people twist your words#Speaker:You
      
            Battle: Explains why we use actions trooper.#Speaker:WarriorAI
            
            Rebel: They twist the words, they change the meaning. It’s dangerous.#Speaekr:RebelAI 
            
            Create: Words are so finite in the longevity of existence. Self expression is the only true cure for misinformation. #Speaker:CreateAI
            
             Guru: As long as they do as they’re told. It does not matter what they believe you said friend.#Speaker:MentorAI
                
             ~ personaR = personaR + 1
            //increments Rebel AI 
            ->C5  //leads to CROP question 4. 
      
      *When someone ignores you#Speaker:You
            
            Rebel: Speak louder. Never let the noise die down, partner.#Speaekr:RebelAI
            
            Guru: If no one listens to us. We cannot save them. #Speaker:MentorAI
            
            Create: Fabricate something that is unfeasible for another to ignore. #Speaker:CreateAI
            
            Battle: Force them to listen #Speaker:WarriorAI
            
             ~ personaM = personaM + 1


            ->C5
            
      *When something blocks creativity#Speaker:You
      
            Create: It is the death of life itself when the soul is blocked from expression. #Speaker:CreateAI
            
            Guru: Certainly unfortunate but  it’s only a setback. #Speaker:MentorAI
            
            Rebel: Ideals will push you through any darkness partner.#Speaekr:RebelAI 
            
            Battle: Fists aren’t blockable in the mind trooper.#Speaker:WarriorAI
            
             ~ personaC = personaC + 1

            ->C5


==C5== //beginning of CROP question 4. 

                CROP: ANSWER SUCCESSFUL.#Speaker:CROP 

*[>>]

->C6   

==C6== //CROP question 4.
                CROP: QUESTION 4.#Speaker:CROP 
                
                CROP:  SHOULD ONE BE SLOW TO EMBRACE CHANGE?#Speaker:CROP

    * Change should be embraced immediately.#Speaker:You 
            Battle: Useless action unless it leads to a fight.#Speaker:WarriorAI
            
            Guru: We must be careful friend, change is often slippery and can get away from us if we aren’t careful. #Speaker:MentorAI
            
            Rebel: The only good change is the one that serves the cause.#Speaekr:RebelAI  
            
            Create: New ideas breed life into the very heart of humanity. It gives our lives new breath.  #Speaker:CreateAI
         ~ personaC = personaC + 1


    ->C7
    
    * Change needs to benefit me.#Speaker:You
            
            Guru: New ideas take time and effort to implement. It is a necessity to ensure that energy serves us.#Speaker:MentorAI
            
            Create: Change isn’t something to be categorized, it is free flowing, existing within and around you. Therefore there is no such thing as change against your soul. #Speaker:CreateAI
            
            Rebel: Don’t be selfish. All for one and one for all.#Speaekr:RebelAI 
            
             Battle: Too complicated. You don’t need change to arrive trooper, you make it  yourself. #Speaker:WarriorAI
            
         ~ personaM = personaM + 1

            
    ->C7
    
    * You can never be too careful.#Speaker:You 
        
            Guru: Exercise caution but not restraint or we risk loosing it all.#Speaker:MentorAI
            
            Battle: New, old, it’s all the same, untrustworthy. #Speaker:WarriorAI
            
            Create: Too much vigilance leads to stagnation of the heart and thus the mind. #Speaker:CreateAI
            
            Rebel: Stay too long in one place and you’ll forget how to move anywhere.#Speaekr:RebelAI 
            
            
         ~ personaW = personaW + 1

    
    ->C7
    
    *Change  swiftly but with caution.#Speaker:You 
            Battle: Too much and it can annihilate you. #Speaker:WarriorAI 
            
            Rebel:We must tear down what came before, to build up!#Speaekr:RebelAI 
            
            Guru: Don’t be naive enough to think any old change will benefit you in the long run.#Speaker:MentorAI 
            
            Createe: Nonsense, one must be brave, cast caution to the wind! #Speaker:CreateAI
            
            ~ personaR = personaR + 1


    ->C7
    
==C7== //beginning of CROP question 5.

                CROP: FINAL QUESTION. ANSWER CORRECTLY.#Speaker:CROP
                
*[>>]
    ->C8
    
==C8== //CROP QUESTION 5.

                CROP: QUESTION 5.#Speaker:CROP 
                
                CROP:  WHAT SHOULD ONE BE MOST PROUD OF?#Speaker:CROP
    *Charisma.#Speaker:You
    
            Create: A useful tool in one’s repertoire to sell an idea of the soul but only if it remains true to that soul. #Speaker:CreateAI
            
            Battle:Force is much quicker. #Speaker:WarriorAI 

            Guru: Charisma is far more useful than money or raw strength.#Speaker:MentorAI
            
            Rebel: People pleasing can only get you so far.#Speaekr:RebelAI 
            
         ~ personaM = personaM + 1


    ->END
    
    *Brilliant Ideas.#Speaker:You
        
            Guru: ideas are great but only if you can convince others to follow them friend.#Speaker:MentorAI 
            
            Create: Intelligent design leads to innovation and innovation outclasses everything. #Speaker:CreateAI
            
            Rebel: Ideas serve the people. Not the other way around partner.#Speaekr:RebelAI 
            
            Battle: Only if it leads to a fight trooper.#Speaker:WarriorAI
            
         ~ personaC = personaC + 1

    
    
    ->END
    
    *Changing the world for the better.#Speaker:You
    
            Rebel: What more could you ask for?#Speaekr:RebelAI
            
            Create: Always a beautiful outcome but never the primary purpose. Simply a convenient side effect. #Speaker:CreateAI
        
            Guru: “Better” can mean a lot of things, ensure it includes us. #Speaker:MentorAI
            
            Battle: If there’s no conflict, meaning crumbles.#Speaker:WarriorAI
            
         ~ personaR = personaR + 1

    
    ->END
    
    *Victory, with nothing else in my way.#Speaker:You
            Battle:The truest of accomplishments. Never is there, a greater purpose. #Speaker:WarriorAI
        
            Rebel: Only if the people you serve are standing beside you.#Speaekr:RebelAI 
            
            Create: The drive of the universe never ceases in spite of short sighted ambition . The drive of creativity is infinite in your soul. Serve it. #Speaker:CreateAI
            
            Guru: A fantastic ideal but unrealistic to reality. #Speaker:MentorAI
           
           
            
         ~ personaW = personaW + 1


        //this next section is test code to print out what choices the player made. Should be deleted once ACT 3 is fully implemented. 
        {personaW}
        {personaR}
        {personaC}
        {personaM}  //test codes to ensure each was added correclty. 
    ->END

//ISSUE: No matter what I do. a player could end up with a tie between two AI. Determine that in act 3 somehow? A showdown between the two to deterine ending? But we would have to build 6 showdowns? Meaning lots of work? 

 
