INCLUDE BarbaristaApi.ink
VAR Perso1 = "Random Dude 1"
VAR Perso2 = "Random Dude 2"
VAR Perso3 = "Sigg"

 ==Avantquete==
{Perso1}: hey salut Sigg
:j'ai besoins d'aide
:j'ai envie d'une bonne boisson
{Perso3}: Ne t'inquietes pas je vais te préparer un très bon Thélixir
:Allez hop au travail
 ~ FinishDialogue("cuisine")
 ->END
 
 
  ==Apresquete==
Sigg : Et voila
Samuel : Haaaa merci
Samuel : Je suis prêt à me battre maintenant
Samuel : Mais où est Vikram ?
 ~ FinishDialogue("gainQuete")
->END
    