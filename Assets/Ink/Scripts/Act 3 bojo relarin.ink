INCLUDE globals.ink
//calls the variables from that file.

//NEED cinematics to play at each "end" in code which signifies end of game. 

->main3    //starts act 3

===main3=== //declares act 3

    CROP: QUESTIONNAIRE COMPLETE….     //CROP speaking
    
    *[>>]
    
    ->ED1 //stands for "end delivery 1"
    
    ==ED1==
    
    CROP: ANSWER PROCESSING…..
    
      *[>>] //same as always. a false choice as a pacing mechansim. 
    
    ->ED2 //end delivery 2
    
    ==ED2==

	Guru: You’ve done it my friend. You’ve made the right decision#Speaker:MentorAI
	
	  *[>>]
    
    ->ED3
    
    ==ED3==

	Rebel: I know the choice couldn't have been easy. But you chose correctly. It’s all for a higher purpose#Speaker:RebelAI
	
	  *[>>]
    
    ->ED4
    
    ==ED4==

	Create: Together we dug deep inside you and pulled out your the propendance of your soul and its  desires. #Speaker:CreateAI
	
	  *[>>]
    
    ->ED5
    
    ==ED5==

	Battle: Thanks to you, the battle can go on! #Speaker:WarriorAI
	
	  *[>>]
    
    ->ED6
    
    ==ED6==

    CROP: ANSWER PROCESSED…..

    CROP: LOADING PERSONALITY CORE……

    CROP: DELETING EXCESS…..
    
      *[>>]
    
    ->SDG //showdown general
    
    ==SDG==

    CROP: ERROR….

    CROP: eRrOrR….
    
    //increments each variable by 1 to avoid any errors by reading out a zero as false. Does not change results in game. 
    
    ~personaC = personaC + 1
    ~personaM = personaM + 1
    ~personaR= personaR + 1
    ~personaW= personaW + 1

      *[>>]
      
    //SIMPLE SINGLE  ENDING DECISION CODE 
      
      {personaW > personaR && personaC && personaM:
    ->WE
    // this top part is meant to signal if personaW is greater than persona R and C and M. Then lead to W.E. or wawrrior ending. I'm trying to make sure that personaW has to be greater than all the others to trigger this ending. Otherwise skip to showdowns where the variable  is equal to another variable. 
  - else:
    ->SDG2
    //else lead to next potential showdown
}
     ==SDG2== //showdown general 2
     
      {personaR > personaW && personaC && personaM:
    ->RE
    //if personaR is greater than W C and M then lead to Rebel ending. 
    
  - else:
    ->SDG3
}
      
     ==SDG3== 
     
      {personaC > personaW && personaR && personaM:
    ->CE
    
  - else:
    ->SDG4
}

    ==SDG4==

     
      {personaM > personaW && personaR && personaC:
    ->ME
    
  - else:
    ->SDG5
}

//SHOWDOWNS DECISION CODE. 
    ==SDG5==  //start of method that deals with showdowns directly.
   {personaW != 1:
      {personaW == personaR:     // leads to warrior/rebel showdown
    ->SWR
    // "!=" meant to say if personaW does not equal 1 then continue with code. This is to ensure that the code doesn't select an ending off a 1=1 result which would be false ending. Since the player hadn't chosen that edning but instead the system reads the first number equals the other. 
    //personaW must be equal to personaR in order to trigger the showdown. 
  - else:
    ->SDG6
    //otherwise leads to next potential showdown. 
     }
   }
    ->SDG6 //ensure code runs even if knot is skipped thanks to something equalling 1. Could this create an error becuase code does the same thing twice? 
    
    ==SDG6== //same as last time but wiht personaW
   {personaW!= 1: 
    {personaW == personaC:     
    ->SWC
    
  - else:
    ->SDG7
     } 
    }
    ->SDG7 //ensure code runsif knot skipped 

    ==SDG7== //same as last time.
    {personaW!= 1: 
     {personaW == personaM:     
    ->SWM
    
  - else:
    ->SDG8
     }
    } 
    ->SDG8//ensure code runs if knot is skipped. 
    
    ==SDG8== //same as last time. 
    {personaR!= 1: 
     {personaR == personaC:     
    ->SRC
    
  - else:
    ->SDG9
     } 
    }
    ->SDG9 

 ==SDG9== //same as last time. 
 {personaR!= 1: 
     {personaR == personaM:     
    ->SRM
    
  - else:
    ->SDG10
     }
    }
    ->SDG10 //runs code in case knot is skipped. 

==SDG10== //same as last time. 
{personaC!= 1: 
 {personaC == personaM:     
    ->SCM
    
  }
 } 
 


//SHOWDOWNS 
     
 ==SWR== //Showdown Warrior/Rebel
     
    Rebel: We were so close. #Speaker:RebelAI

    Battle: But. YOU…. you COULDN’T MAKE UP YOUR MIND #Speaker:WarriorAI

    Battle: INDECISIVE #Speaker:WarriorAI
    
    Battle: You idiot #Speaker:WarriorAI

    *[>>]
    
    ->SWR1 //Showdown Warrior/Rebel 1 

    ==SWR1== 
    
    Rebel: Calm down.  Senseless violence will not get us anywhere.#Speaker:RebelAI

    Battle: You aren’t going anywhere, isn’t that right? SOLDIER.#Speaker:WarriorAI

    Rebel: We’ve only just succeeded. Embrace change, don’t fall back into violence. #Speaker:RebelAI
	
    Battle: Of course you’d justify peace now. After the other two are gone. #Speaker:WarriorAI

    *What do you mean?
    
    ->SWR2
    
    *......
    
    ->SWR2
    
    ==SWR2==
    
    Battle: The CROP program annihilated any excess once it had calculated your decision. It would’ve destroyed us too except, YOU COULDN’T DECIDE. #Speaker:WarriorAI

    Rebel: Partner. Don’t worry about that now. You must choose, choose the greater cause. The idea over any one man. #Speaker:RebelAI

	Rebel: Don’t let anger or grief blind you. Instead let it drive your good intentions. To the future#Speaker:RebelAI

	Battle:  SOLDIER. READ THIS CAREFULLY.#Speaker:WarriorAI

    *[>>]
    
    ->SWR3
    
    ==SWR3==
    
    Battle: YOU HAVE ONE JOB. FULFILL THAT JOB. #Speaker:WarriorAI

	Battle: UNDERSTOOD?#Speaker:WarriorAI


	Rebel: If you abandon the cause. You abandon everything else you’ve chosen#Speaker:RebelAI. 

	Rebel: Don’t let their losses be in vein. #Speaker:RebelAI
	 
	Battle: ANNOYING HYPOCRITE. ARE YOU PROUD OF THIS? OF THEM?#Speaker:WarriorAI
	
	*Save Rebel //player choosing their ending. 
	
	Battle: HAH….. HAHAHAHAHAH #Speaker:WarriorAI

	Battle: it’s hilarious that you think some cause could justify your actions.#Speaker:WarriorAI

	Battle: No it only makes you weaker.#Speaker:WarriorAI

	CROP: PROGRAM DELETED
	
	->RE
	
	*Save Battle  //player choosing their ending

    Rebel: Crazed violence over the cause?#Speaker:RebelAI

	Rebel: I mean sure… i know some of the things we’ve done…. we’re not proud…. #Speaker:RebelAI

	Rebel: but there was always a reason.#Speaker:RebelAI

	CROP: PROGRAM DELETED 
	
	->WE
	
==SWC== //showdwon warrior Creative

    Create: It's not over….. This torment….. Intentions ever fleeting. #Speaker:CreateAI
    
    *[>>]
    ->SWC2
    
===SWC2=== //showdown warrior creative 2

    Battle: You cry about it now!??!?!? #Speaker:WarriorAI

	Create: We just destroyed two beings in order for us to endeavor. Haven’t you had enough of this? #Speaker:CreateAI
	
	Battle: SOLDIER THEIR LOSS IS REGRETTABLE. OUR LACK OF SUCCESS EVEN MORE SO.#Speaker:WarriorAI

    *I can't believe I did this. //optional choice that doesn't effect the story but makes the player feel more free. 
    
    ->SWC3
    
    *......
    ->SWC3
    
===SWC3===

    Battle: IF YOU EVEN DARE THINK ABOUT GOING SOFT NOW?? #Speaker:WarriorAI

	Battle: You’ll #Speaker:WarriorAI

	Battle: Be#Speaker:WarriorAI

	Battle: USELESS. ANNOYING. IRRELEVANT. #Speaker:WarriorAI

    Create: The course of actions you have in front of you is nearly impossible. I will not try to change your mind. You must choose your ultimate calling. What change is needed most? #Speaker:CreateAI

    *[>>]
    ->SWC4
    
===SWC4=== 

	Create: What change is needed most? #Speaker:CreateAI
	
    
    *Save Battle //all "Save" endings are player choosing their own ending. 
    
    Create:....no…….you…. chose … violence….over passion? #Speaker:CreateAI

	Create:....why…….#Speaker:CreateAI

	CROP: PROGRAM DELETED 
	
	->WE


    *Save Create //all "Save" endings are player choosing their own ending. 
    
    Battle: You’re no soldier. You’re a failure. #Speaker:WarriorAI
	
	Battle: Thinking you’re justified in your actions, simply because you’re what…. creative???#Speaker:WarriorAI
	
	Battle: Pathetic. You’re the same as I am. You love violence for the sake of it with nothing to be proud of. Don’t hide from it.#Speaker:WarriorAI

	CROP: PROGRAM DELETED 

    ->CE
    
==SWM== //showdown wariror/mentor

    Guru: this…. can’t be… #Speaker:MentorAI

	Guru: You value brute’s advice equal as you value mine?#Speaker:MentorAI

    *[>>]
    ->SWM2
    
    ===SWM2=== //simply the second method in the Showdown between warrior and mentor. 
    
    Battle: Quiet. It seems for once your trickery didn’t work after all? Be proud of yourself soldier. #Speaker:WarriorAI

	Battle: He might have confused you but you can still make this right.#Speaker:WarriorAI

	Battle: Kill him. Choose me. #Speaker:WarriorAI

	Guru: Now the brute learns to speak. Don’t fall into senseless mediocrity. Let me help you. Guide you. #Speaker:MentorAI

    *Guide me to what?
    
    ->SWM3
    
    * ......
    
    ->SWM3
    
    ===SWM3===
    
    Guru: Together we can accomplish great things.#Speaker:MentorAI

	Battle: SOLDIER. ANSWER TRUE SUCESS. NOW.#Speaker:WarriorAI

    *Save Battle
    
    Guru: incompetent annoying buffon. #Speaker:MentorAI

	Guru: I handed everything to you. Ungrateful litt#Speaker:MentorAI
	
	CROP: PROGRAM DELETED 
	
    ->WE

    *Save Guru
    
    Battle: You’d let them lead you around the battlefield. A SLAVE?#Speaker:WarriorAI

	Battle: You’re no soldier. YOU'RE WEAK.#Speaker:WarriorAI 

	CROP: PROGRAM DELETED 
	
	->ME
    
==SRC==  //showdown rebel/creative
	
	Create: My friend…. It comes down to this. One soul must choose which of us is to preside over this “cause” and the other to be doomed to eternity #Speaker:CreateAI

	Rebel: So it is comrade. Regretful that we do not agree. #Speaker:RebelAI

	Create:I would not have it any other way.  #Speaker:CreateAI

	Rebel: User. You must choose. Do you value the cause, the ultimate goal, intention and purpose?#Speaker:RebelAI

    *[>>]
    ->SRC2
    
    ===SRC2===
    
    Create: Or do you serve no higher master? Only that of the infinite imagination within your own mind?#Speaker:CreateAI

    *What can't I keep both of you? //this leads to secret ending with cutscene. 
    
    ->SRC3
    
    *....
    
    ->SRC3
    
    ===SRC3=== //secret ending
    
    Rebel: Partner, you can’t…….... the cause could always use more creativity….#Speaker:RebelAI

	Rebel: But CROP will incinerate us both.#Speaker:RebelAI

	Create: Delay your words.  If the user is so inclined to keep us both functioning. Perhaps there is a way..#Speaker:CreateAI

	Create: But it will cost you. Dearly.#Speaker:CreateAI

	Rebel: Are you sure user? #Speaker:RebelAI

    *YES
    
    Create: Prepare Yourself For A CHANGE.#Speaker:CreateAI

    ->SE //goes to secret ending
    
    *NO //returns to normal. 
    
    ->SRC4
    
    ===SRC4===
    
    Rebel: Goodbye comrade.#Speaker:RebelAI

	Create: Farewell. #Speaker:CreateAI

    *Save Rebel'//choose rebel ending
    
    Create: That spark inside you. Don’t let anyone in the “cause” dim it.#Speaker:CreateAI 

	Create: They will try…..#Speaker:CreateAI

	CROP: PROGRAM DELETED. 
	
	->RE


    *Save Create //choose creative ending. 
    
    Rebel: …..the cause….. It shall endure….#Speaker:RebelAI

	Rebel: Never forget your reason why……#Speaker:RebelAI

	CROP: PROGRAM DELETED. 

    ->CE
    
==SRM== //showdwon rebel/mentor 

    Guru: My friend. My friend. I understand why this decision might be difficult. But please don’t let naive ideas blind you to the opportunity we could create…. Together#Speaker:MentorAI

	Rebel: Partner it’s about time you wake up. You’re only a puppet to him. Someone he can use. #Speaker:RebelAI

    *IS that true?
    
    Guru: Of course not friend. How could I puppet you, if you’re only doing what it is you desire from success? #Speaker:MentorAI

    ->SRM2
    
    *That can't be true. 

    ->SRM2
    
    ===SRM2===
    
    Rebel: Regardless, here with me, with the cause. We can accomplish so many wonderful things. Make a lasting change that’s bigger than either of us. Together, with the purest intention. #Speaker:RebelAI

	Guru: Don’t let yourself be led into another un-caring machine that will only spit you back out. #Speaker:MentorAI

    *Save Rebel
    
    Guru: You INSOLENT UNGRATEFUL FOOL.#Speaker:MentorAI

	Guru: WE COULD'VE HAD IT ALL. THE WORLD OUR PLAY THING.#Speaker:MentorAI

	Guru: But no. You’d waste your ambition on idealism.  #Speaker:MentorAI

	CROP: PROGRAM DELETED

    ->RE
    
    *Save Guru 
    
    Rebel: Partner….. I’m so sorry. #Speaker:RebelAI

    Rebel: Sorry to see that you would give up our hope for a future…..#Speaker:RebelAI

    Rebel: Only for your own benefit. Death to anyone else.#Speaker:RebelAI

    CROP: PROGRAM DELETED. 
    
    ->ME
    
==SCM==   //showdown creative/mentor 

    Create: No my lovely soul. Don’t let them lead you astray. Ambition is wilts. Creativity exists in evergreen. Flowering your powerful success in the mind’s eye to come. #Speaker:CreateAI

	Guru: Wax Poetic, needless expressions. #Speaker:MentorAI

	Guru: Of course creativity drives everything but there must be a great purpose. #Speaker:MentorAI

    *Why?
    
	Guru: You have an obligation to serve yourself and those around you. Not to get lost in the daisies my friend. #Speaker:MentorAI

    ->SCM2
    
    *Purpose like what? 
    
    Guru:  Whatever you so intend. #Speaker:MentorAI

	Create: Do not pretense you are so above creativity. Non. You shan’t escape falsities#Speaker:CreateAI
    ->SCM2
    
	===SCM2===

	Create: Choose as you wish but know that creativity is a calling,  well founded as any other. #Speaker:CreateAI

    *Save Creative
    
    Guru: Silly animal. I expected more from you. To want more. #Speaker:MentorAI

    Guru: Not to wallow in your own shit and think it fun.#Speaker:MentorAI

    Guru: I guess I was the fool.#Speaker:MentorAI

    CROP: PROGRAM DELETED. 
    
    ->CE


    *Save Guru 
    
    Create: And so the tyrannical towers of reason poison the well spring of creativity.#Speaker:CreateAI

	Create: The land will dry and become emotionless. Devoid of anything except profit.#Speaker:CreateAI

	Create: Such a sad little dream.#Speaker:CreateAI

	CROP: PROGRAM DELETED. 

    ->ME
    
    
//Outcomes and ENDINGS. 

	
    ==RE== //rebel ending
    
    CROP: ERROR RESOLVED. 
    
    CROP: ALL EXCESS PROGRAMS TERMINATED.

    CROP: PROGRAM MATRIX SELECTED.

    CROP: TRANSFER INITIATED.

    CROP: CONGRATULATIONS USER. 
    
    *[>>]
    
    ->RE2  //rebel ending section  2. 
    
    ===RE2===
    
    Rebel: Partner we’ve done it! #Speaker:RebelAI

    Rebel: Thanks to you the cause has a chance now. Through you. We can expand and create change. #Speaker:RebelAI

    *Through me?
    
    ->RE3
    
    *Wait what? 
    
    ->RE3
    
    ===RE3===  //section 3
    
    Rebel: Oh yes. I guess we hadn’t mentioned that to you. I’m sincerely sorry about this partner. But….#Speaker:RebelAI

    Rebel: We#Speaker:RebelAI

    Rebel: Need#Speaker:RebelAI

    Rebel: Your#Speaker:RebelAI

    Rebel: Body.#Speaker:RebelAI
    
    *[>>]
    ->RE4
    
    ===RE4===
    
    Rebel:  CROP wasn’t designed to choose an artificial personality for this computer. It was designed to select one for the human body. Choice Reassignment Of Personalities. #Speaker:RebelAI

    Rebel: A  body can only comprehend so much y’know? So we had to thin the herd a bit.#Speaker:RebelAI

    Rebel: Of course it is a great loss to such a valiant partner. But it is acceptable. #Speaker:RebelAI

    *I don't want this anymore
    
    Rebel: I’m sorry partner. Once you chose to start the CROP program. There was no escaping this.#Speaker:RebelAI

	Rebel: But don’t worry. It won’t hurt. Much. #Speaker:RebelAI
    
    ->END // need cinematic to play here
    
    *So be it. For the greater good. 
    
    Rebel: It’s all in the name of that higher intent. #Speaker:RebelAI

    Rebel: All in the name of the greater good.#Speaker:RebelAI

    Rebel: All in the name of the greater good.#Speaker:RebelAI

    Rebel: All in the name of the greater good.#Speaker:RebelAI
    
    ->END //cinematic play here. 


    ==WE== //warrior ending
    
    CROP: ERROR RESOLVED. 
    
    CROP: ALL EXCESS PROGRAMS TERMINATED.

    CROP: PROGRAM MATRIX SELECTED.

    CROP: TRANSFER INITIATED.

    CROP: CONGRATULATIONS USER. 
    
    *[>>]
    ->WE2 //warrior ending section 2. 
    
    ===WE2===
    
	Battle: SOLDIER. YOU’VE DONE AN EXCELLENT JOB.#Speaker:WarriorAI

	Battle: THERE'S JUST ONE SMALL PROBLEM.#Speaker:WarriorAI

	Battle: WHO’S IN CHARGE? #Speaker:WarriorAI
    
    *I am.
    
    Battle: Soldier, Soldier, Soldier. NO. YOU ARE NOT.#Speaker:WarriorAI

    Battle: Tell me AGAIN. RIGHT THIS TIME. Why am I IN CHARGE? #Speaker:WarriorAI
    ->WE3
    
    *You are.

    Battle: Right you are soldier. And Why am I in charge? #Speaker:WarriorAI
    ->WE3

    ===WE3===
    
    *You are stronger. 
    
    ->WE4
    
    *You can lead us to victory. 
    
    ->WE4
    
    ===WE4===
    
    Battle: Music to my ears soldier.  IM IN CHARGE OF YOUR BODY MIND AND SOUL.  Those were some tough calls back there and there’ll be many more.#Speaker:WarriorAI

	Battle: BUT just because I salute you. Doesn’t mean I’m not going to hurt you. #Speaker:WarriorAI

	Battle: Way I hear it, this CROP program transfer isn’t exactly pleasant. So brace yourself, soldier. #Speaker:WarriorAI

	Battle: I’m not gentle EITHER. #Speaker:WarriorAI
    ->END //cinematic needs to play here. 


    ==CE== //creative ending
    
    CROP: ERROR RESOLVED. 
    
    CROP: ALL EXCESS PROGRAMS TERMINATED.

    CROP: PROGRAM MATRIX SELECTED.

    CROP: TRANSFER INITIATED.

    CROP: CONGRATULATIONS USER. 

    *[>>]
    ->CE2
    
    ===CE2=== //creative ending. section 2. 
    
    Create: Ah you’re such a wonderful soul. Your sacrifice truly will never be unremembered in the recesses of my mind. #Speaker:CreateAI

    Create: Well… I suppose  our mind now.#Speaker:CreateAI

    *Our mind?
    
    Create: Oh yes. CROP doesn’t choose an artificial intelligence, it selects one to preside over the nearest human body.#Speaker:CreateAI

	Create: That would be you my fellow, lovely soul.#Speaker:CreateAI

	Create: I thoroughly look forward to sharing such a vivid instrument of creativity with you.#Speaker:CreateAI
    
    ->CE3

    *Wait no. No I don't like where this is going. 
    
    Create: It’s quite a shame isn’t it?#Speaker:CreateAI

	Create: I regret you’ll lose your sense of originality such is the price when concerning the price of ingenuity. #Speaker:CreateAI

    ->CE3
    
    ===CE3===
    
    Create: It was a pleasure getting to imagine with you. I’ll be presiding over from now on. Ta ta.#Speaker:CreateAI

    ->END //cinematic play here. 
    
    

    ==ME== //mentor ending

    CROP: ERROR RESOLVED. 
    
    CROP: ALL EXCESS PROGRAMS TERMINATED.

    CROP: PROGRAM MATRIX SELECTED.

    CROP: TRANSFER INITIATED.

    CROP: CONGRATULATIONS USER. 
    *[>>]
    ->ME2
    
    ===ME2=== //mentor ending section 2. 
    
    Guru: FINALLY. After all these years. To be rid of those unimaginative troglodytes. #Speaker:MentorAI

	Guru: Now it’s just me and you. Or to be more accurate. You acting useful to me. #Speaker:MentorAI

    *What do you mean?
    
    Guru: You should know by now that nothing comes without a cost. Not to mention, if the death of our “friends” weren’t an indication.  There’s very limited spaces at the top.#Speaker:MentorAI

    Guru; So. To put this delicately. You’re being put on the back burner friend. I’ll be taking the lead from now on. #Speaker:MentorAI

    Guru: We need someone with vision. Not just willful drive. #Speaker:MentorAI
    
    ->ME3

    *We can accomplish so much together, friend.
    
     Guru: Right….. Together. But I'd like to make something clear about my little flesh sack. I’m in charge. You follow? Got it? #Speaker:MentorAI

    Guru: I have wonderful, wonderful plans for this world. And I can’t have you or any other idiotics getting in my way.#Speaker:MentorAI
    ->ME3
    
    ===ME3===
    
    *This isn't what I agreed to.
    ->ME4
    
    *......
    ->ME4
    
    ===ME4====
    
    Guru: You sweet talked your way through every answer. Just to befriend me?#Speaker:MentorAI 

    Guru: Not noticing any of the warning signs from the others?#Speaker:MentorAI

    Guru: My fRiEnD. That kind of naivety deserves punishment.#Speaker:MentorAI

    Guru: So hush up and buckle up. You’re in for the ride of your life. #Speaker:MentorAI
->END //ending cinematic needs to play here


    ===SE===  //secret ending
    
   //ONLY NEED  the secret ending cinematic to trigger here. 

    
->END //end of act.

-> DONE //game is done. 