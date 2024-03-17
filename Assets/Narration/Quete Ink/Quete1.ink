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
 Sigg: Bon voici votre quete.
:Recherche d'un filon de Alatium pour fabriquer des armures
 
 
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
 ~playSound("I_Cava")
 : ça va ?

 
 
 
 - Perso == "Elira" : // dialogue de presentation de Elira
  {Perso}: Je ne suis pas censé pouvoir etre affecté à cette quête.
  
  
  
 
 - Perso == "Saori" : // dialogue de presentation de Saori
  {Perso}:Je ne suis pas censé pouvoir etre affecté à cette quête.
  
  
 
 - Perso == "Vikram" : // dialogue de presentation de Vikram
 ~playSound("I_BonjourVikram")
  {Perso}:Hey c'est Vikram 
  : ça va le vieux ?
 
 }
 
 ~EtapePresentationPerso = EtapePresentationPerso + 1
->Avantquete.BeforePresentation



 ==ReactionQuete(Perso)==
 // ici faut du texte en fonction de la personne 
 {
 
 
 
 
 - Perso == "Samuel" : // dialogue reaction de quete de Samuel
  ~playSound("I_SamuelHappy2")
 {Perso}:Samuel, d’un ton <bounce>enjoué</bounce>, dit qu’il à hâte d’aller dans les montagnes pour extraire ce minerais car il a toujours voulu voir le minerais à l’état brut. 
 ~playSound("I_SamuelHappy")
 :Grâce à ces minerais, ils pourront forger d’autre armes et améliorer leur armures en vue du combat contre l’Oméga, ce dernier ayant détruit la ville basse il y a plusieurs années.
 
 
 
 - Perso == "Elira" : // dialogue de reaction de quete de Elira
  {Perso}:Je ne suis pas censé pouvoir etre affecté à cette quête.
  
  
  
 
 - Perso == "Saori" : // dialogue de reaction de quete de Saori
  {Perso}:Je ne suis pas censé pouvoir etre affecté à cette quête.
  
  
 
 - Perso == "Vikram" : // dialogue de reaction de quete de Vikram
 ~playSound("I_VikramRaler")
  {Perso}:Vikram déclara d’un air sombre que contrairement à Samuel, il n’a pas hâte d’aller se geler les miches sous la grêle. Très peu pour lui. Il n’aime pas le froid et en tremble d’avance.
  
 
 }
 
 
  ~EtapeReactionnPerso = EtapeReactionnPerso + 1
->Avantquete.BeforeReaction
 
 
 
 
 
 
  ==Apresquete==
  
  ->Init(false)
  
  =PastInit
~fadeIn(Perso1,0)
~fadeIn(Perso2,0)

  
  ~playSound("I_Siggprepafini")
Sigg: Et voila
:Bon courage
 ~ FinishDialogue("gainQuete")
 
 
 ~fadeOut(Perso1,0)
~fadeOut(Perso2,0)
->END
    