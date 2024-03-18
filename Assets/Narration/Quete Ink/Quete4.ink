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
 Sigg: Bon voici votre quete
 :Mission de sauvetage d'un avant-poste isolé
 
 
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
 ~playSound("I_Samuelsadly")
 {Perso}:Samuel est inquièt pour les aventuriers et l’équipe blessée dans l’avant poste isolé dans la forêt bioluminescente car une brume toxique peut se lever et aggraver leur état.

 
 
 
 - Perso == "Elira" : // dialogue de reaction de quete de Elira
 ~playSound("I_EliraSad")
  {Perso}:Élira est inquiète pour les aventurier et l’équipe blessée dans l’avant post isolé dans la forêt bioluminescente car une brume toxique peut se lever et aggraver leur état ainsi que les Évispar qui sont très toxique. 

  
  
  
 
 - Perso == "Saori" : // dialogue de reaction de quete de Saori
  ~playSound("I_SaoriFascinate")
  {Perso}:Saori aime beaucoup cette forêt car elle est fasciné par les plantes bioluminescentes qui s’y trouvent mais n’aime pas les Évispars qui la dégoutent.
  
  
 
 - Perso == "Vikram" : // dialogue de reaction de quete de Vikram
 ~playSound("I_VikramBeGood")
  {Perso}:Vikram dit qu’il ne s’inquiète pas pour eux et souhaite élimin
  er un un maximum de monstre pour les venger et éviter que d’autre ne les gène plus tard.
 
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
    