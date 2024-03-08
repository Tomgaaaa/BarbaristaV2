INCLUDE BarbaristaApi.ink
INCLUDE vnsup_api.ink
VAR Perso1 = "Random Dude 1"
VAR Perso2 = "Random Dude 2"

VAR EtapePresentationPerso = 1
VAR EtapeReactionnPerso = 1





==Presentation(Perso)==

~fadeIn(Perso,1)



// ici faut du texte en fonction de la personne 
 {
 
 
 
 
 - Perso == "Samuel" : // dialogue de presentation de Samuel
 {Perso}: Hey c'est Samuel
 : ça va ?
 
 
 
 - Perso == "Elira" : // dialogue de presentation de Elira
  {Perso}: Hey c'est Elira
  :je suis encore qu'un concept?
  
  
  
 
 - Perso == "Saori" : // dialogue de presentation de Saori
  {Perso}: Hey c'est Saori
  : je suis une scientifique ... enfin je crois
  
  
 
 - Perso == "Vikram" : // dialogue de presentation de Vikram
  {Perso}: Hey c'est Vikram
  : ça va le vieux ?
 
 }
 
 ~EtapePresentationPerso = EtapePresentationPerso + 1
->Avantquete


 ==Avantquete==
 
 Sigg: Ho un nouveau client
 
 
{
-EtapePresentationPerso == 1 : 
->Presentation(Perso1)
-EtapePresentationPerso == 2 : 
->Presentation(Perso2)
 
 }
 
 Sigg: Bon voici votre quete
 : je vais vous préparer de superbes Thélixir
 
 
 
 
 {
-EtapeReactionnPerso == 1 : 
->Presentation(Perso1)
-EtapeReactionnPerso == 2 : 
->Presentation(Perso2)
 
 }
 
//{Perso1}: hey salut Sigg
//:j'ai besoins d'aide
//:j'ai envie d'une bonne boisson
//{Perso3}: Ne t'inquietes pas je vais te préparer un très bon Thélixir
//:Allez hop au travail




 ~ FinishDialogue("cuisine")
 ->END
 
 
 ==ReactionQuete(Perso)==
 // ici faut du texte en fonction de la personne 
 {
 
 
 
 
 - Perso == "Samuel" : // dialogue reaction de quete de Samuel
 {Perso}:Hu facile
 
 
 
 - Perso == "Elira" : // dialogue de reaction de quete de Elira
  {Perso}:Bon y go ?
  
  
  
 
 - Perso == "Saori" : // dialogue de reaction de quete de Saori
  {Perso}:Let's go c'est parti
  
  
 
 - Perso == "Vikram" : // dialogue de reaction de quete de Vikram
  {Perso}:Un simple échauffement
 
 }
 
 
  ~EtapeReactionnPerso = EtapeReactionnPerso + 1
->Avantquete
 
 
  ==Apresquete==
Sigg : Et voila
Samuel : Haaaa merci
Samuel : Je suis prêt à me battre maintenant
Samuel : Mais où est Vikram ?
 ~ FinishDialogue("gainQuete")
->END
    