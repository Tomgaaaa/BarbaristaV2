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
 
 
 // dialogue avant que les personnages reagissent a la quete*
 ~playSound("I_ExpressionSigg")
 Sigg: Bon voici votre quete
 :Opération d'extraction de souffre pour fabriquer de la poudre noir
 
 
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
~playSound("I_ArrivéPnj")


// ici faut du texte en fonction de la personne 
 {
 
 
 
 
 - Perso == "Samuel" : // dialogue de presentation de Samuel
 ~playSound("I_BonjourSamuel")
 {Perso}:Hey c'est Samuel
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
 ~playSound("I_SamuelHappy")
 {Perso}:Samuel trouve l’objectif de leur mission très utile car le souffre leur permettra de confectionner de la poudre noir pour des explosifs car ça à montré leur efficacité sur l’Oméga à l’époque.

 
 
 
 - Perso == "Elira" : // dialogue de reaction de quete de Elira
  ~playSound("I_Eliraaimepas")
  {Perso}:Élira n’aime pas ce biome car elle à peur qu’il s’étende et ne perturbe l’harmonie naturel du reste de la planète car le souffre est très nocif pour les plantes et les animaux (sauf ceux qui s’y trouve) si sa voute cristalline venait à lâcher.

  
  
  
 
 - Perso == "Saori" : // dialogue de reaction de quete de Saori
 ~playSound("I_SaoriSatisfaite")
  {Perso}:Saori est satisfaite d’aller dans ce biome car elle pourras récupérer des échantillons de souffre pour savoir s’ils peuvent en faire plus qu’une poudre explosive et développer un engrais puissant.
  
  
 
 - Perso == "Vikram" : // dialogue de reaction de quete de Vikram
  ~playSound("I_VikramImpatient")
  {Perso}:Vikram est impatient d’y aller car il à toujours voulu avoir des crocs de Cresholds car ils sont très stylé et font du feu quand les les frappe les uns contre les autres.
 
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
    