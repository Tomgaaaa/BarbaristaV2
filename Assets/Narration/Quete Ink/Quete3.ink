INCLUDE BarbaristaApi.ink
INCLUDE vnsup_api.ink
VAR Perso1 = "Random Dude 1"
VAR Perso2 = "Random Dude 2"

VAR EtapePresentationPerso = 1
VAR EtapeReactionnPerso = 1

VAR previousChemin = "vide"

===Init(isAvantQuete)=== // fonction appellé aux debut du chargement de la scene VN pour deplacer Perso2 et le Flip
 ~moveTo(Perso2,"Droite",0)
 ~flipX(Perso2)
 
 {
 -isAvantQuete == true:
-> Avantquete.PastInit // permet de renvoyer au chemin AvantQuete

 -isAvantQuete == false: // permet de renvoyer au chemin ApresQuete
-> Apresquete.PastInit
}



 ==Avantquete== // chapitre appellé lors du brief de la quete avant la cuisine


->Init(true) // initialise la scene 



=PastInit // chemin permettant d'esquiver l'Init 


//dialogue avant que les personnages arrivent 
 ~playSound("I_ArrivéPnjAlerte")
 ~playSound("I_ArrivéPnj")
 Sigg: Haaa les voila qui arrivent
 
 
 
 ->BeforePresentation // renvoie au chemin ci dessous 
 

 =BeforePresentation // chemin appellé lorsqu'on a finis une presentation pour faire la deuxime presentation / passer a la suite
 
{
-EtapePresentationPerso == 1 : // si je suis a l'etape 1, je presente le Perso 1
->Presentation(Perso1)
-EtapePresentationPerso == 2 :  // si je suis a l'etape 2, je presente le Perso 2
->Presentation(Perso2)
 }
  // si j'ai passé l'étape 2, je ne rentre pas dans le if et je passe à la suite
 
 
 // dialogue avant que les personnages reagissent a la quete
 ~playSound("I_ExpressionSigg")
 Sigg:Chasse de Deshrog pour alimenter les chaufferies
 
 
 ->BeforeReaction //renvoi au chemin ci dessous
 
 =BeforeReaction // chemin appellé lorsque les personnages ont finis de réagir
 {
-EtapeReactionnPerso == 1 : // si je suis à l'étape 1, le personnage 1 réagite à la quete
->ReactionQuete(Perso1)
-EtapeReactionnPerso == 2 : // si je suis à l'étape 2, le personnage 2 réagite à la quete
->ReactionQuete(Perso2)
 
 }
 
 
 
 // dialogue apres la reaction des personnages
 ~playSound("I_SiggHappy")
Sigg:Allez hop au travail




 ~ FinishDialogue("cuisine")
 ->END
 
 ==Presentation(Perso)==

~fadeIn(Perso,1)



// ici faut du texte en fonction de la personne 
 {
 
 
 
 
 - Perso == "Samuel" : // dialogue de presentation de Samuel
 ~playSound("I_BonjourSamuel")
  ~stopSound("BarAlatea")
 {Perso}:Hey c'est Samuel
 ~playSound("I_Cava")
 :Ca va ?

 
 
 
 - Perso == "Elira" : // dialogue de presentation de Elira
 ~playSound("I_BonjourElira")
  {Perso}:Hey c'est Elira
  :La forme ?
  
  
  
 
 - Perso == "Saori" : // dialogue de presentation de Saori
 ~playSound("I_SaoriBonjour")
  {Perso}:Hey c'est Saori
  :Comment tu vas Sigg ?
  
  
 
 - Perso == "Vikram" : // dialogue de presentation de Vikram
  ~playSound("I_BonjourVikram")
  {Perso}:Hey c'est Vikram
  :Ca va le vieux ?
 }
 
 ~EtapePresentationPerso = EtapePresentationPerso + 1
->Avantquete.BeforePresentation



 ==ReactionQuete(Perso)==
 // ici faut du texte en fonction de la personne 
 {
 
 
 
 
 - Perso == "Samuel" : // dialogue reaction de quete de Samuel
 ~playSound("I_Samuelraler")
 {Perso}:Samuel n’aime pas trop l’idée d’aller profondément dans le basalte car avec l’activité volcanique il y a des risque d’effondrement.
 
 
 
 - Perso == "Elira" : // dialogue de reaction de quete de Elira
 ~playSound("I_EliraPassive")
  {Perso}Élira est plutôt neutre par rapport à cette quête mais inquiète pour le fait que l’activité volcanique  car les Srégols seront plus actifs et elle n’as pas envie d’en tuer si ce n’est pas nécessaire.
  
  
  
 
 - Perso == "Saori" : // dialogue de reaction de quete de Saori
  ~playSound("I_SaoriPasContente")
  {Perso}:Saori dit qu’elle n’est pas très à l’aise à l’idée d’aller dans un biome chaud, elle supporte mal la chaleur et le feu lui rappel de mauvais souvenir lors de l’attaque de l’Oméga.
  
  
 
 - Perso == "Vikram" : // dialogue de reaction de quete de Vikram
  ~playSound("I_VikramContent")
  {Perso}:Vikram à hâte d’aller affronter des Deshrogs car on lui a promis une nouvelle arme dès que les fonderies auraient de quoi fonctionner.
 
 }
 
 
  ~EtapeReactionnPerso = EtapeReactionnPerso + 1
->Avantquete.BeforeReaction
 
 
 
 
  ==Apresquete==
  
  ->Init(false)
  
  =PastInit
  
  ~fadeIn(Perso1,0)
~fadeIn(Perso2,0)
  
  ~playSound("I_Siggprepafini")
Sigg : Et voila
:Bon courage
 ~ FinishDialogue("gainQuete")
 
 
  ~fadeOut(Perso1,0)
~fadeOut(Perso2,0)
->END
    