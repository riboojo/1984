INCLUDE globals.ink
 
 -> main2 //calls main class for act 2
 
 ===main2=== 
 //"a" for AI answers to player. "I" for general AI commentary.   "C" for crop questions 

    Felicitations. #Speaker:CreateAI //beginning of on screen text

    *[>>] //Fake choice pacing mechanisim. So lines are delivered slowly. 
    -> I1 
 
 ==I1==  //General AI commentary 1
    The weakling is destroyed now. #Speaker:WarriorAI
     *[>>] 
    ->I2

 ==I2==  //General AI commentary 2
    We did it only through your help. #Speaker:MentorAI
    *[>>] 
    ->I3
 
 ==I3== //General AI commentary 3
    There’s still so much to do, partner. #Speaker:RebelAI
    *[>>] 
    ->I4

 ==I4==
    Your soul must judge. #Speaker:CreateAI
    *[>>] 
    ->I5
 
 ==I5==
    What do you stand for? #Speaker:RebelAI
    *[>>] 
    ->I6
 
 ==I6==
    Which of us will die? #Speaker:WarriorAI
    *[>>] 
    ->I7
 
 ==I7==
    Answer the questions correctly and you’ll determine your own fate as much as ours. #Speaker:MentorAI
    ->I8

 ==I8==
 
CONDITIONING AND REGULATION OF PROGRAMS LOADED. #Speaker:CROP
PERSONALITY SELECTION PROCESS INITIATING..... #Speaker:CROP
*[>>] 
->C1

==C1==  // Beginning of CROP questions

QUESTION 1... #Speaker:CROP 
DO OTHERS GENERALLY HAVE GOOD INTENTIONS? #Speaker:CROP

    *Of course not #Speaker:You
            History is full of those who underestimated others. #Speaker:MentorAI
            You're always in someone's crosshairs. #Speaker:WarriorAI
            People do. The system doesn’t. #Speaker:RebelAI 
            Bland binary choice. There’s always room for grays. #Speaker:CreateAI
             ~ personaW = personaW + 1   //increments AI Variable based on choice. This will be used to determine ending in ACT 3. 
            ->C2 //leads to CROP question 2. 
//playtest wether need to pace these out or not. 

    *Generally, yea #Speaker:You
            Foolish, easy way to get bit trooper. #Speaker:WarriorAI
            Be careful you don’t let someone control you my friend. #Speaker:MentorAI
            Hold on. It’s the system that instills negativity. #Speaker:RebelAI 
            Is it their intentions? Or their thoughts that are good? #Speaker:CreateAI
             ~ personaR = personaR + 1
 //increments rebel variable. rebel response. 
            ->C2
    
    *Assume the best but prepare for the worst #Speaker:You
            Don't worry how people choose to lead their lives, my friend. Simply steer their course. #Speaker:MentorAI
            Don’t let them disappoint you trooper.  #Speaker:WarriorAI
            Assumptions often narrow your ways of thinking. Be cautious but don’t assume. #Speaker:CreateAI
            Don’t get caught up in fantasies. Control yourself. #Speaker:RebelAI 
             ~ personaM = personaM + 1
 //mentor incremented. 
            ->C2    //leads to CROP quesiton 2. 
                
    *There's never a good or bad choice. #Speaker:You
            Great. Now you decide nothing. #Speaker:WarriorAI
            Isn’t that a bit too nihilistic? Don’t you think? #Speaker:MentorAI
            There are only various options and outcomes. #Speaker:CreateAI
            Always focus on the outcome patner.#Speaker:RebelAI 
            ~ personaC = personaC + 1
                 //incrments creative. 
            ->C2
    
    
==C2== //calls method with beginning of CROP question 2. 
ANSWER RECORDED. #Speaker:CROP
*[>>]
->C3

==C3== //method with crop question 2.

QUESTION 2... #Speaker:CROP 
HOW SHOULD ONE MEASURE SUCCESS? #Speaker:CROP 

    * Be the last one standing #Speaker:You 
            Decisive, true victory. #Speaker:WarriorAI
            Victory is useless without a cause partner. #Speaker:RebelAI 
            Such a quaint and brutish response for such an intriguing soul. #Speaker:CreateAI 
            The last one standing  is nothing without more to follow him. #Speaker:MentorAI
            ~ personaW = personaW + 1 //increments warrior. 
            ->C4   //leads to CROP question 3.
                
    *Who really put in the effort to succeed?#Speaker:You 
            It isn’t about success but the ability to thrive. You need control of the situation in the end friend. #Speaker:MentorAI
            Effort is the center of everything.#Speaker:CreateAI
            Only action matters. #Speaker:WarriorAI
            Results are everything, partner.#Speaker:RebelAI 
             ~ personaC = personaC + 1 //increments Creative 
            ->C4   //leads to CROP question 3
    
    *I got what I wanted and left.#Speaker:You 
            You should never leave a challenge trooper.#Speaker:WarriorAI 
            Does it serve the cause?#Speaker:RebelAI
            Always the means to an end, friend. #Speaker:MentorAI
            Sure product is important, but the process. That’s so much more predominant. #Speaker:CreateAI
            ~personaM = 2 //increments mentor
            ->C4

    * The outcome should serve the purpose#Speaker:You
        	The strongest purpose, VICTORY! #Speaker:WarriorAI
            Does it serve you most of all though friend?#Speaker:MentorAI
            True, however the purpose exists without the outcome. It is innate to one’s soul. #Speaker:CreateAI
            Designing real and definite change is the ultimate purpose.#Speaker:RebelAI 
             ~ personaR = personaR + 1//incrments rebel. 
            ->C4


==C4== //Calls beginning of CROP question 3. 
QUESTION 3.#Speaker:CROP
WHAT IS GENERALLY MOST ANNOYING?#Speaker:CROP
  
      * Someone who doesn’t follow through#Speaker:You
            There’s always a reason for the soul to falter. The only thought that matters is whether it pertains to you? #Speaker:CreateAI
          	Your word is bond. Never break it. #Speaker:WarriorAI
            If you can’t trust others. Do it yourself, partner.#Speaker:RebelAI 
            Unfortunately incompetence exists. An immortal pest.#Speaker:MentorAI 
             ~ personaW = personaW + 1 //increments warrior. 
            ->C5  //leads to CROP questio 4.

      *When people twist your words#Speaker:You
            Explains why we use actions trooper. #Speaker:WarriorAI
            They twist the words, they change the meaning. It’s dangerous. #Speaker:RebelAI 
            Words are so finite in the longevity of existence. Self expression is the only true cure for misinformation. #Speaker:CreateAI
            As long as they do as they’re told. It does not matter what they believe you said friend. #Speaker:MentorAI
             ~ personaR = personaR + 1
            //increments Rebel AI 
            ->C5  //leads to CROP question 4. 
      
      *When someone ignores you #Speaker:You
            Speak louder. Never let the noise die down, partner. #Speaker:RebelAI
            If no one listens to us. We cannot save them. #Speaker:MentorAI
            Fabricate something that is unfeasible for another to ignore. #Speaker:CreateAI
            Force them to listen #Speaker:WarriorAI
             ~ personaM = personaM + 1
            ->C5
            
      *When something blocks creativity #Speaker:You
            It is the death of life itself when the soul is blocked from expression. #Speaker:CreateAI
            Certainly unfortunate but  it’s only a setback. #Speaker:MentorAI
            Ideals will push you through any darkness partner. #Speaker:RebelAI 
            Fists aren’t blockable in the mind trooper. #Speaker:WarriorAI
             ~ personaC = personaC + 1
            ->C5


==C5== //beginning of CROP question 4. 
ANSWER SUCCESSFUL. #Speaker:CROP 
*[>>]
->C6   

==C6== //CROP question 4.
QUESTION 4... #Speaker:CROP 
 SHOULD ONE BE SLOW TO EMBRACE CHANGE? #Speaker:CROP

    * Change should be embraced immediately. #Speaker:You 
            Useless action unless it leads to a fight. #Speaker:WarriorAI
            We must be careful friend, change is often slippery and can get away from us if we aren’t careful. #Speaker:MentorAI
            The only good change is the one that serves the cause. #Speaker:RebelAI  
            New ideas breed life into the very heart of humanity. It gives our lives new breath.  #Speaker:CreateAI
            ~ personaC = personaC + 1
            ->C7
    
    * Change needs to benefit me. #Speaker:You
            New ideas take time and effort to implement. It is a necessity to ensure that energy serves us. #Speaker:MentorAI
            Change isn’t something to be categorized, it is free flowing, existing within and around you. Therefore there is no such thing as change against your soul. #Speaker:CreateAI
            Don’t be selfish. All for one and one for all. #Speaker:RebelAI 
            Too complicated. You don’t need change to arrive trooper, you make it  yourself. #Speaker:WarriorAI
            ~ personaM = personaM + 1   
            ->C7
    
    * You can never be too careful. #Speaker:You 
            Exercise caution but not restraint or we risk loosing it all. #Speaker:MentorAI
            New, old, it’s all the same, untrustworthy. #Speaker:WarriorAI
            Too much vigilance leads to stagnation of the heart and thus the mind. #Speaker:CreateAI
            Stay too long in one place and you’ll forget how to move anywhere. #Speaker:RebelAI 
            ~ personaW = personaW + 1
            ->C7
    
    *Change  swiftly but with caution. #Speaker:You 
            Too much and it can annihilate you. #Speaker:WarriorAI 
            We must tear down what came before, to build up! #Speaker:RebelAI 
            Don’t be naive enough to think any old change will benefit you in the long run. #Speaker:MentorAI 
            Nonsense, one must be brave, cast caution to the wind! #Speaker:CreateAI
            ~ personaR = personaR + 1
            ->C7
    
==C7== //beginning of CROP question 5.
FINAL QUESTION. ANSWER CORRECTLY. #Speaker:CROP
*[>>]
->C8
    
==C8== //CROP QUESTION 5.
QUESTION 5...#Speaker:CROP 
WHAT SHOULD ONE BE MOST PROUD OF?#Speaker:CROP
    
    *Charisma. #Speaker:You
            A useful tool in one’s repertoire to sell an idea of the soul but only if it remains true to that soul. #Speaker:CreateAI
            Force is much quicker. #Speaker:WarriorAI
            Charisma is far more useful than money or raw strength. #Speaker:MentorAI
            People pleasing can only get you so far. #Speaker:RebelAI 
            ~ personaM = personaM + 1
            ->END
    
    *Brilliant Ideas. #Speaker:You
            ideas are great but only if you can convince others to follow them friend. #Speaker:MentorAI 
            Intelligent design leads to innovation and innovation outclasses everything. #Speaker:CreateAI
            Ideas serve the people. Not the other way around partner. #Speaker:RebelAI 
            Only if it leads to a fight trooper. #Speaker:WarriorAI
            ~ personaC = personaC + 1
            ->END
    
    *Changing the world for the better. #Speaker:You
            What more could you ask for? #Speaker:RebelAI
            Always a beautiful outcome but never the primary purpose. Simply a convenient side effect. #Speaker:CreateAI
            “Better” can mean a lot of things, ensure it includes us. #Speaker:MentorAI
            If there’s no conflict, meaning crumbles. #Speaker:WarriorAI
            ~ personaR = personaR + 1
            ->END
    
    *Victory, with nothing else in my way. #Speaker:You
            The truest of accomplishments. Never is there, a greater purpose. #Speaker:WarriorAI
            Only if the people you serve are standing beside you.#Speaker:RebelAI 
            The drive of the universe never ceases in spite of short sighted ambition . The drive of creativity is infinite in your soul. Serve it. #Speaker:CreateAI
            A fantastic ideal but unrealistic to reality. #Speaker:MentorAI
            ~ personaW = personaW + 1


        //this next section is test code to print out what choices the player made. Should be deleted once ACT 3 is fully implemented. 
        {personaW}
        {personaR}
        {personaC}
        {personaM}  //test codes to ensure each was added correclty. 
    ->END

//ISSUE: No matter what I do. a player could end up with a tie between two AI. Determine that in act 3 somehow? A showdown between the two to deterine ending? But we would have to build 6 showdowns? Meaning lots of work? 

 
