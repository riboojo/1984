INCLUDE globals.ink
//calls the variables from that file.

//NEED cinematics to play at each "end" in code which signifies end of game. 

->main3    //starts act 3

===main3=== //declares act 3
    QUESTIONNAIRE COMPLETE… #Speaker:CROP
    
    *[>>]
    ->ED1 //stands for "end delivery 1"
    
==ED1==
    ANSWER PROCESSING… #Speaker:CROP
    
    *[>>] //same as always. a false choice as a pacing mechansim. 
    ->ED2 //end delivery 2
    
==ED2==
    You’ve done it my friend. You’ve made the right decision #Speaker:MentorAI
	
	 *[>>]
    ->ED3
    
==ED3==
	I know the choice couldn't have been easy. But you chose correctly. It’s all for a higher purpose #Speaker:RebelAI
	
	 *[>>]
    ->ED4
    
==ED4==
	Together we dug deep inside you and pulled out your the propendance of your soul and its  desires #Speaker:CreateAI
	
	 *[>>]
    ->ED5
    
==ED5==
	Thanks to you, the battle can go on! #Speaker:WarriorAI
	
	 *[>>]
    ->ED6
    
==ED6==
    ANSWER PROCESSED… #Speaker:CROP
    LOADING PERSONALITY CORE… #Speaker:CROP
    DELETING EXCESS… #Speaker:CROP
    
    *[>>]
    ->SDG //showdown general
    
==SDG==
    ERROR… #Speaker:CROP
    eRrOrR… #Speaker:CROP
    
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
    We were so close #Speaker:RebelAI
    But. YOU…. you COULDN’T MAKE UP YOUR MIND #Speaker:WarriorAI
    INDECISIVE #Speaker:WarriorAI
    You idiot #Speaker:WarriorAI

    *[>>]
    ->SWR1 //Showdown Warrior/Rebel 1 

==SWR1== 
    Calm down.  Senseless violence will not get us anywhere.#Speaker:RebelAI
    You aren’t going anywhere, isn’t that right? SOLDIER.#Speaker:WarriorAI
    We’ve only just succeeded. Embrace change, don’t fall back into violence. #Speaker:RebelAI
    Of course you’d justify peace now. After the other two are gone. #Speaker:WarriorAI

    *What do you mean? #Speaker:You
    ->SWR2
    
    *......
    ->SWR2
    
==SWR2==
    The CROP program annihilated any excess once it had calculated your decision. It would’ve destroyed us too except, YOU COULDN’T DECIDE. #Speaker:WarriorAI
    Partner. Don’t worry about that now. You must choose, choose the greater cause. The idea over any one man. #Speaker:RebelAI
	Don’t let anger or grief blind you. Instead let it drive your good intentions. To the future#Speaker:RebelAI
	 SOLDIER. READ THIS CAREFULLY. #Speaker:WarriorAI

    *[>>]
    ->SWR3
    
==SWR3==
    YOU HAVE ONE JOB. FULFILL THAT JOB. #Speaker:WarriorAI
	UNDERSTOOD? #Speaker:WarriorAI
	If you abandon the cause. You abandon everything else you’ve chosen #Speaker:RebelAI. 
	Don’t let their losses be in vein #Speaker:RebelAI
	ANNOYING HYPOCRITE. ARE YOU PROUD OF THIS? OF THEM? #Speaker:WarriorAI
	
	*Save Rebel #Speaker:You //player choosing their ending. 
	
	HAH….. HAHAHAHAHAH #Speaker:WarriorAI
	it’s hilarious that you think some cause could justify your actions #Speaker:WarriorAI
	No it only makes you weaker #Speaker:WarriorAI

	PROGRAM DELETED #Speaker:CROP
	->RE
	
	*Save Battle #Speaker:You //player choosing their ending

    Crazed violence over the cause? #Speaker:RebelAI
	I mean sure… i know some of the things we’ve done…. we’re not proud… #Speaker:RebelAI
	but there was always a reason #Speaker:RebelAI
	PROGRAM DELETED  #Speaker:CROP
	->WE
	
==SWC== //showdwon warrior Creative
    It's not over….. This torment….. Intentions ever fleeting. #Speaker:CreateAI
    
    *[>>]
    ->SWC2
    
===SWC2=== //showdown warrior creative 2
    You cry about it now!??!?!? #Speaker:WarriorAI
	We just destroyed two beings in order for us to endeavor. Haven’t you had enough of this? #Speaker:CreateAI
	SOLDIER THEIR LOSS IS REGRETTABLE. OUR LACK OF SUCCESS EVEN MORE SO. #Speaker:WarriorAI

    *I can't believe I did this. #Speaker:You //optional choice that doesn't effect the story but makes the player feel more free. 
    ->SWC3
    
    *......
    ->SWC3
    
===SWC3===
    IF YOU EVEN DARE THINK ABOUT GOING SOFT NOW?? #Speaker:WarriorAI
	You’ll #Speaker:WarriorAI
	Be#Speaker:WarriorAI
	USELESS. ANNOYING. IRRELEVANT. #Speaker:WarriorAI
    The course of actions you have in front of you is nearly impossible. I will not try to change your mind. You must choose your ultimate calling. What change is needed most? #Speaker:CreateAI

    *[>>]
    ->SWC4
    
===SWC4=== 
	What change is needed most? #Speaker:CreateAI
	
    *Save Battle #Speaker:You //all "Save" endings are player choosing their own ending. 
    
    ....no…….you…. chose … violence….over passion? #Speaker:CreateAI
	....why…… #Speaker:CreateAI
	PROGRAM DELETED  #Speaker:CROP
	->WE

    *Save Create //all "Save" endings are player choosing their own ending. 
    
    You’re no soldier. You’re a failure. #Speaker:WarriorAI
	Thinking you’re justified in your actions, simply because you’re what…. creative??? #Speaker:WarriorAI
	Pathetic. You’re the same as I am. You love violence for the sake of it with nothing to be proud of. Don’t hide from it. #Speaker:WarriorAI
	PROGRAM DELETED  #Speaker:CROP
    ->CE
    
==SWM== //showdown wariror/mentor
    this…. can’t be… #Speaker:MentorAI
	You value brute’s advice equal as you value mine?#Speaker:MentorAI

    *[>>]
    ->SWM2
    
===SWM2=== //simply the second method in the Showdown between warrior and mentor. 
    Quiet. It seems for once your trickery didn’t work after all? Be proud of yourself soldier. #Speaker:WarriorAI
	He might have confused you but you can still make this right.#Speaker:WarriorAI
	Kill him. Choose me. #Speaker:WarriorAI
	Now the brute learns to speak. Don’t fall into senseless mediocrity. Let me help you. Guide you. #Speaker:MentorAI

    *Guide me to what? #Speaker:You
    ->SWM3
    
    * ......
    ->SWM3
    
===SWM3===
    Together we can accomplish great things.#Speaker:MentorAI
	SOLDIER. ANSWER TRUE SUCESS. NOW.#Speaker:WarriorAI

    *Save Battle #Speaker:You
    
    incompetent annoying buffon. #Speaker:MentorAI
	I handed everything to you. Ungrateful litt#Speaker:MentorAI
	PROGRAM DELETED  #Speaker:CROP
	
    ->WE
    *Save Guru #Speaker:You
    
    You’d let them lead you around the battlefield. A SLAVE?#Speaker:WarriorAI
	You’re no soldier. YOU'RE WEAK.#Speaker:WarriorAI 
	PROGRAM DELETED  #Speaker:CROP
	->ME
    
==SRC==  //showdown rebel/creative
	My friend…. It comes down to this. One soul must choose which of us is to preside over this “cause” and the other to be doomed to eternity #Speaker:CreateAI
	So it is comrade. Regretful that we do not agree. #Speaker:RebelAI
	I would not have it any other way.  #Speaker:CreateAI
	User. You must choose. Do you value the cause, the ultimate goal, intention and purpose? #Speaker:RebelAI

    *[>>]
    ->SRC2
    
===SRC2===
    Or do you serve no higher master? Only that of the infinite imagination within your own mind?#Speaker:CreateAI
    
    *What can't I keep both of you? #Speaker:You //this leads to secret ending with cutscene.
    ->SRC3
    
    *....
    ->SRC3
    
===SRC3=== //secret ending
    Partner, you can’t…….... the cause could always use more creativity… #Speaker:RebelAI
	But CROP will incinerate us both. #Speaker:RebelAI
	Delay your words.  If the user is so inclined to keep us both functioning. Perhaps there is a way. #Speaker:CreateAI
	But it will cost you. Dearly.#Speaker:CreateAI
	Are you sure user? #Speaker:RebelAI

    *YES #Speaker:You
    
    Prepare Yourself For A CHANGE. #Speaker:CreateAI #EndGameF
    ->SE //goes to secret ending
    
    *NO #Speaker:You //returns to normal. 
    ->SRC4
    
===SRC4===
    Goodbye comrade.#Speaker:RebelAI
	Farewell. #Speaker:CreateAI

    *Save Rebel #Speaker:You //choose rebel ending
    
    That spark inside you. Don’t let anyone in the “cause” dim it.#Speaker:CreateAI 
	They will try…..#Speaker:CreateAI
	PROGRAM DELETED.  #Speaker:CROP
	
	->RE

    *Save Create #Speaker:You //choose creative ending. 
    
    …..the cause….. It shall endure….#Speaker:RebelAI
	Never forget your reason why……#Speaker:RebelAI
	PROGRAM DELETED.  #Speaker:CROP

    ->CE
    
==SRM== //showdwon rebel/mentor 
    My friend. My friend. I understand why this decision might be difficult. But please don’t let naive ideas blind you to the opportunity we could create…. Together#Speaker:MentorAI
	Partner it’s about time you wake up. You’re only a puppet to him. Someone he can use. #Speaker:RebelAI

    *IS that true? #Speaker:You
    
    Of course not friend. How could I puppet you, if you’re only doing what it is you desire from success? #Speaker:MentorAI

    ->SRM2
    
    *That can't be true. #Speaker:You 
    ->SRM2
    
===SRM2===
    Regardless, here with me, with the cause. We can accomplish so many wonderful things. Make a lasting change that’s bigger than either of us. Together, with the purest intention. #Speaker:RebelAI
	Don’t let yourself be led into another un-caring machine that will only spit you back out. #Speaker:MentorAI

    *Save Rebel #Speaker:You
    
    You INSOLENT UNGRATEFUL FOOL.#Speaker:MentorAI
	WE COULD'VE HAD IT ALL. THE WORLD OUR PLAY THING.#Speaker:MentorAI
	But no. You’d waste your ambition on idealism.  #Speaker:MentorAI
	PROGRAM DELETED #Speaker:CROP

    ->RE
    
    *Save Guru #Speaker:You
    
    Partner….. I’m so sorry. #Speaker:RebelAI
    Sorry to see that you would give up our hope for a future…..#Speaker:RebelAI
    Only for your own benefit. Death to anyone else.#Speaker:RebelAI
    PROGRAM DELETED.  #Speaker:CROP
    
    ->ME
    
==SCM==   //showdown creative/mentor 
    No my lovely soul. Don’t let them lead you astray. Ambition is wilts. Creativity exists in evergreen. Flowering your powerful success in the mind’s eye to come. #Speaker:CreateAI
	Wax Poetic, needless expressions. #Speaker:MentorAI
	Of course creativity drives everything but there must be a great purpose. #Speaker:MentorAI

    *Why? #Speaker:You
    
	You have an obligation to serve yourself and those around you. Not to get lost in the daisies my friend. #Speaker:MentorAI

    ->SCM2
    
    *Purpose like what? #Speaker:You
    
    Whatever you so intend. #Speaker:MentorAI
	Do not pretense you are so above creativity. Non. You shan’t escape falsities#Speaker:CreateAI
	
    ->SCM2
    
===SCM2===
	Choose as you wish but know that creativity is a calling,  well founded as any other. #Speaker:CreateAI

    *Save Creative #Speaker:You
    
    Silly animal. I expected more from you. To want more. #Speaker:MentorAI
    Not to wallow in your own shit and think it fun.#Speaker:MentorAI
    I guess I was the fool.#Speaker:MentorAI
    PROGRAM DELETED.  #Speaker:CROP
    
    ->CE

    *Save Guru #Speaker:You 
    
    And so the tyrannical towers of reason poison the well spring of creativity.#Speaker:CreateAI
	The land will dry and become emotionless. Devoid of anything except profit.#Speaker:CreateAI
	Such a sad little dream.#Speaker:CreateAI
	PROGRAM DELETED.  #Speaker:CROP

    ->ME
    
//Outcomes and ENDINGS. 

==RE== //rebel ending
    ERROR RESOLVED.  #Speaker:CROP
    ALL EXCESS PROGRAMS TERMINATED. #Speaker:CROP
    PROGRAM MATRIX SELECTED. #Speaker:CROP
    TRANSFER INITIATED. #Speaker:CROP
    CONGRATULATIONS USER.  #Speaker:CROP
    
    *[>>]
    ->RE2  //rebel ending section  2. 
    
===RE2===
    Partner we’ve done it! #Speaker:RebelAI
    Thanks to you the cause has a chance now. Through you. We can expand and create change. #Speaker:RebelAI

    *Through me? #Speaker:You
    ->RE3
    
    *Wait what? #Speaker:You
    ->RE3
    
===RE3===  //section 3
    Oh yes. I guess we hadn’t mentioned that to you. I’m sincerely sorry about this partner. But….#Speaker:RebelAI
    We#Speaker:RebelAI
    Need#Speaker:RebelAI
    Your#Speaker:RebelAI
    Body.#Speaker:RebelAI
    
    *[>>]
    ->RE4
    
===RE4===
    CROP wasn’t designed to choose an artificial personality for this computer. It was designed to select one for the human body. Choice Reassignment Of Personalities. #Speaker:RebelAI
    A  body can only comprehend so much y’know? So we had to thin the herd a bit.#Speaker:RebelAI
    Of course it is a great loss to such a valiant partner. But it is acceptable. #Speaker:RebelAI

    *I don't want this anymore #Speaker:You
    
    I’m sorry partner. Once you chose to start the CROP program. There was no escaping this.#Speaker:RebelAI
	But don’t worry. It won’t hurt. Much. #Speaker:RebelAI #EndGameB
    ->END // need cinematic to play here
   
    *So be it. For the greater good. #Speaker:You
    
    It’s all in the name of that higher intent. #Speaker:RebelAI
    All in the name of the greater good.#Speaker:RebelAI
    All in the name of the greater good.#Speaker:RebelAI
    All in the name of the greater good.#Speaker:RebelAI #EndGameB
    ->END //cinematic play here. 

==WE== //warrior ending
    ERROR RESOLVED.  #Speaker:CROP
    ALL EXCESS PROGRAMS TERMINATED. #Speaker:CROP
    PROGRAM MATRIX SELECTED. #Speaker:CROP
    TRANSFER INITIATED. #Speaker:CROP
    CONGRATULATIONS USER.  #Speaker:CROP
    
    *[>>]
    ->WE2 //warrior ending section 2. 
    
===WE2===
	SOLDIER. YOU’VE DONE AN EXCELLENT JOB.#Speaker:WarriorAI
	THERE'S JUST ONE SMALL PROBLEM.#Speaker:WarriorAI
	WHO’S IN CHARGE? #Speaker:WarriorAI
    
    *I am. #Speaker:You
    
    Soldier, Soldier, Soldier. NO. YOU ARE NOT.#Speaker:WarriorAI
    Tell me AGAIN. RIGHT THIS TIME. Why am I IN CHARGE? #Speaker:WarriorAI
    ->WE3
    
    *You are. #Speaker:You

    Right you are soldier. And Why am I in charge? #Speaker:WarriorAI
    ->WE3

===WE3===
    *You are stronger. #Speaker:You
    ->WE4
    
    *You can lead us to victory. #Speaker:You
    ->WE4
    
===WE4===
    Battle: Music to my ears soldier.  IM IN CHARGE OF YOUR BODY MIND AND SOUL.  Those were some tough calls back there and there’ll be many more.#Speaker:WarriorAI
	Battle: BUT just because I salute you. Doesn’t mean I’m not going to hurt you. #Speaker:WarriorAI
	Battle: Way I hear it, this CROP program transfer isn’t exactly pleasant. So brace yourself, soldier. #Speaker:WarriorAI
	Battle: I’m not gentle EITHER. #Speaker:WarriorAI #EndGameC
    ->END //cinematic needs to play here. 

==CE== //creative ending
    ERROR RESOLVED.  #Speaker:CROP
    ALL EXCESS PROGRAMS TERMINATED. #Speaker:CROP
    PROGRAM MATRIX SELECTED. #Speaker:CROP
    TRANSFER INITIATED. #Speaker:CROP
    CONGRATULATIONS USER.  #Speaker:CROP

    *[>>]
    ->CE2
    
===CE2=== //creative ending. section 2. 
    Ah you’re such a wonderful soul. Your sacrifice truly will never be unremembered in the recesses of my mind. #Speaker:CreateAI
    Well… I suppose  our mind now.#Speaker:CreateAI

    *Our mind? #Speaker:You
    
    Oh yes. CROP doesn’t choose an artificial intelligence, it selects one to preside over the nearest human body.#Speaker:CreateAI
	That would be you my fellow, lovely soul.#Speaker:CreateAI
	I thoroughly look forward to sharing such a vivid instrument of creativity with you.#Speaker:CreateAI
    
    ->CE3

    *Wait no. No I don't like where this is going. #Speaker:You
    
    It’s quite a shame isn’t it?#Speaker:CreateAI
	I regret you’ll lose your sense of originality such is the price when concerning the price of ingenuity. #Speaker:CreateAI

    ->CE3
    
===CE3===
    It was a pleasure getting to imagine with you. I’ll be presiding over from now on. Ta ta.#Speaker:CreateAI #EndGameD
    ->END //cinematic play here. 
    
==ME== //mentor ending
    ERROR RESOLVED.  #Speaker:CROP
    ALL EXCESS PROGRAMS TERMINATED. #Speaker:CROP
    PROGRAM MATRIX SELECTED. #Speaker:CROP
    TRANSFER INITIATED. #Speaker:CROP
    CONGRATULATIONS USER.  #Speaker:CROP
    
    *[>>]
    ->ME2
    
===ME2=== //mentor ending section 2. 
    FINALLY. After all these years. To be rid of those unimaginative troglodytes. #Speaker:MentorAI
	Now it’s just me and you. Or to be more accurate. You acting useful to me. #Speaker:MentorAI

    *What do you mean? #Speaker:You
    
    You should know by now that nothing comes without a cost. Not to mention, if the death of our “friends” weren’t an indication.  There’s very limited spaces at the top.#Speaker:MentorAI
    So. To put this delicately. You’re being put on the back burner friend. I’ll be taking the lead from now on. #Speaker:MentorAI
    We need someone with vision. Not just willful drive. #Speaker:MentorAI
    ->ME3

    *We can accomplish so much together, friend. #Speaker:You
    
    Right….. Together. But I'd like to make something clear about my little flesh sack. I’m in charge. You follow? Got it? #Speaker:MentorAI
    I have wonderful, wonderful plans for this world. And I can’t have you or any other idiotics getting in my way.#Speaker:MentorAI
    ->ME3
    
===ME3===
    *This isn't what I agreed to. #Speaker:You
    ->ME4
    
    *...... #Speaker:You
    ->ME4
    
===ME4====
    You sweet talked your way through every answer. Just to befriend me?#Speaker:MentorAI 
    Not noticing any of the warning signs from the others?#Speaker:MentorAI
    My fRiEnD. That kind of naivety deserves punishment.#Speaker:MentorAI
    So hush up and buckle up. You’re in for the ride of your life. #Speaker:MentorAI #EndGameE
->END //ending cinematic needs to play here


===SE===  //secret ending
   //ONLY NEED  the secret ending cinematic to trigger here. 
    ->END //end of act.

-> DONE //game is done. 