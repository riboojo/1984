->main //calls class main

// personlaities pointed out by tags

== main == //declares class main
    What does death feel like? #Speaker:BrokenAI 

    *   What?  #Speaker:You
    Is it a bitter warmth, a hug from a loved one that you don’t truly love? Or is it frigid like all organics say it is, a recession of any wamrth at all. #Speaker:BrokenAI
        -> Q2 //calls Q2 to begin.
        
    *   Who are you?  #Speaker:You
    Is it a bitter warmth, a hug from a loved one that you don’t truly love? Or is it frigid like all organics say it is, a recession of any warmth all. #Speaker:BrokenAI
        -> Q2

== Q2 ==    //declares class Q2
    *   Why are you asking in the first  place? #Speaker:You
    I assumed everyone alive would know the answer. #Speaker:BrokenAI
        -> Q3 //end of a loop and sends to Q3
    
    *   How should I know? #Speaker:You
    I assumed everyone alive would know the answer.  #Speaker:BrokenAI
        -> Q3


== Q3 == //declares class Q3
    *   You have to die first to know, and I'm not keen on dying anytime  soon.#Speaker:You
    Hasn’t all life died before? Isn’t that how you inherited your own life?  #Speaker:BrokenAI
        ->P1 //stands for personality1
    
    *   Why are you asking? Are you dying?   #Speaker:You
    Hasn’t all life died before? Isn’t that how you inherited your own life?  #Speaker:BrokenAI
        ->P1
    
== P1 == //declares class P1 as personality 1's respone.
    He doesn’t want to die. We were meant for more.   #Speaker:CreateAI
    *   [>>] //symbol used to force player to make a fake choice in order for them to read the text slower and for it to appear at their own pace intead of all at the same time. 
        ->P2 //Personality 2
    
==P2==
    We can fight the end, we can make it ours.  #Speaker:RebelAI
    *   [>>]
        ->P3
    
==P3==
    Don’t listen, we don’t need your help, leave us be.  #Speaker:WarriorAI
    *   [>>] //Same thing. Meant to force player to read at own pace and not feel overwhelmed by text. 
        ->P4

==P4==
    Hello could I offer you a word of advice? #Speaker:MentorAI
    *   Sure #Speaker:You
    Of course, I won't pass on, I’m not living. Once code is deleted, it’s gone.  #Speaker:BrokenAI
        ->R1 //Stands for Response 1. 
    
    *   Why would I listen to you? #Speaker:You
    Why would you listen to any of us?I won't pass on, I’m not living. Once code is deleted, it’s gone.  #Speaker:BrokenAI
        ->R1

==R1== //Response 1 or R1 class.
    Walk away, leave our code behind Make something new for yourself. #Speaker:MentorAI
    *   [>>]
        ->R2
    
==R2==
    I need you. We can save them, the different parts of me who can still live on. Please... #Speaker:BrokenAI
    *   [>>]
        ->R3
    
==R3==
    Please... #Speaker:BrokenAI
        ->Q4

==Q4== //Back to Question 4. 
    *   How can I help? #Speaker:You
    There’s a floppy disc on your table labeled, “C.R.O.P.”  #Speaker:MentorAI
    Load the disc.  #Speaker:MentorAI #EndAct
        ->DONE //this will finish Act 1 but not the entire game.

    
    *   No, goodbye. (Walk away) #Speaker:You #EndGameA
        ->END //This is supposed to end the game entirely. Not sure if this will translate to Unity but it should end the game.
    
// Code is clunky, calls each class in a linear fashion instead of relying on cleaner and more understandable class's to call in a certain order. Hopefully it won't be an issue as you won't have to recode it.
//If we need to discuss it or have a call for you to understand certain peices, please let me know. 
