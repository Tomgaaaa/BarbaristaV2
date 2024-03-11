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
 Sigg: Bon voici votre quete
 :Récolte d'Aloalgue pour fabriquer des baumes
 
 
 ->BeforeReaction //renvoi au chemin ci dessous
 
 =BeforeReaction // chemin appellé lorsque les personnages ont finis de réagir
 {
-EtapeReactionnPerso == 1 : // si je suis à l'étape 1, le personnage 1 réagite à la quete
->ReactionQuete(Perso1)
-EtapeReactionnPerso == 2 : // si je suis à l'étape 2, le personnage 2 réagite à la quete
->ReactionQuete(Perso2)
 
 }
 
 
 
 // dialogue apres la reaction des personnages
Sigg:Allez hop au travail




 ~ FinishDialogue("cuisine")
 ->END
 
 ==Presentation(Perso)==

~fadeIn(Perso,1)



// ici faut du texte en fonction de la personne 
 {
 
 
 
 
 - Perso == "Samuel" : // dialogue de presentation de Samuel
 {Perso}:Hey c'est Samuel
 :Ca va ?

 
 
 
 - Perso == "Elira" : // dialogue de presentation de Elira
  {Perso}:Hey c'est Elira
  :La forme ?
  
  
  
 
 - Perso == "Saori" : // dialogue de presentation de Saori
  {Perso}:Hey c'est Saori
  :Comment tu vas Sigg ?
  
  
 
 - Perso == "Vikram" : // dialogue de presentation de Vikram
  {Perso}:Hey c'est Vikram
  :Ca va le vieux ?
 }
 
 ~EtapePresentationPerso = EtapePresentationPerso + 1
->Avantquete.BeforePresentation



 ==ReactionQuete(Perso)==
 // ici faut du texte en fonction de la personne 
 {
 
 
 
 
 - Perso == "Samuel" : // dialogue reaction de quete de Samuel
 {Perso}:Samuel a hâte d’aller chercher des Aloalgues car dans ce biome les lacs ont des pierres aimanté, des magnétites et il aimerai en récupéré pour sa collection personnelle.
 
 
 
 - Perso == "Elira" : // dialogue de reaction de quete de Elira
  {Perso}:Élira espère vraiment que les Kerusculas ne leur poseront pas problème, elle n’a pas envie d’en tuer alors que ce n’est pas une nécessité.
  :Elle a hâte que tous soit prêt pour aller chasser l’Oméga qui fait beaucoup de mal à la faune et la flore.
  
  
  
 
 - Perso == "Saori" : // dialogue de reaction de quete de Saori
  {Perso}:Saori s’exprime sur le fait qu’elle est contente d’aller récolter les Aloalgues car leur propriétés curative l’intéresse pour ses recherches mais l’idée d’aller dans une zone avec des Keruscula ne l’enchante pas car elle n’aime pas ces créatures.
  
  
 
 - Perso == "Vikram" : // dialogue de reaction de quete de Vikram
  {Perso}:Vikram n’a pas du tout envie d’aller là-bas et de tomber sur les Keruscula, il les a en horreur car il est tombé dans un des nids et les bébé Kérusculas ont été partout sur lui. 
  :Ça l’a <shake>traumatisé.</shake>
 
 }
 
 
  ~EtapeReactionnPerso = EtapeReactionnPerso + 1
->Avantquete.BeforeReaction
 
 
 
 
  ==Apresquete==
  
  ->Init(false)
  
  =PastInit
Sigg: Et voila
:Bon courage
 ~ FinishDialogue("gainQuete")
->END
    