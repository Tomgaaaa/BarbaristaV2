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
 
 
 // dialogue avant que les personnages reagissent a la quete
 ~playSound("I_ExpressionSigg")
 Sigg: Bon voici votre quete
 :Récolte de plante toxique pour création d'enduit pour armes
 
 
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
  ~playSound("I_Samuelraler")
 {Perso}:Samuel dit qu’il n’aime pas vraiment ce biome car il n’aime pas la majorité des plantes carnivore et n’aimerai pas finir digéré par l’une d’entre elles.

 
 
 
 - Perso == "Elira" : // dialogue de reaction de quete de Elira
  ~playSound("I_EliraCool")
  {Perso}:Élira aime beaucoup ce biome car pour elle c’est là que la nature y est la plus diversifiée et foisonnante et cela lui permettra de peindre certaine des fleures et plantes.

  
  
  
 
 - Perso == "Saori" : // dialogue de reaction de quete de Saori
 ~playSound("I_SaoripasContente")
  {Perso}:Saori est pas très fan à l’idée d’aller dans la jungle car elle n’aime pas les créatures qui s’y trouve, il y fait très humide et elle ne voudrai pas salir sont équipement car le sol est boueux à certains endroit.

  
  
 
 - Perso == "Vikram" : // dialogue de reaction de quete de Vikram
 ~playSound("I_VikramMotivé")
  {Perso}:Vikram à hâte d’y aller car récolter ces plantes lui permettra de renforcer son poignard qu’il tien de ses parents mort contre l’Oméga.
 
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
    