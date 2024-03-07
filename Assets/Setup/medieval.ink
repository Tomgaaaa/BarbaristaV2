INCLUDE vnsup_api.ink
INCLUDE custom.ink
 

VAR coffee = false
VAR arme = ""

~ changeBg("Castle")

C'est après une longue nuit de sommeil qu'un grand cri vous tira de votre torpeur.
~ show("witch")
~ face("witch", "angry")
~ placeTo("witch", "center")
witch:Ma gemme de pouvoir ! Eleanor, ma gemme de pouvoir !
~ moveTo("witch", "left", 0.4)
~ show("eleanor")
~ face("eleanor", "anxious")
eleanor:Oui ? <b>Hein ?</b> <wave>Quoi ?</wave>
~ face("witch", "")
witch:Elle a disparu ! Il faut la retrouver !
~ face("eleanor", "")
eleanor:Et vous avez une idée de qui aurait pu faire ça ?
witch:Bien sûr ! C'est Smordok, il vient toujours ici me dérober mes objets de valeur
:Il cache un trésor en haut de la montagne, qu'il fait protéger par un dragon
:Rends toi là bas et ramène moi ma gemme ou mes pouvoirs auront à jamais disparu !
eleanor:Très bien... Je m'équipe et j'y vais...
witch:Fait vite, je ne te laisser aller dans chaque pièce qu'une seule fois avant ton départ !
~ fadeBg("Mountain", 1)
~ hide("witch")
~ hide("eleanor")
-> main_room

=== main_room ===
{Vous voilà au centre du château, déterminé à vous préparer pour ce qui vous attends.|Visiblement vous êtes de retour au centre du château.}
 * [Aller en cuisine] -> cuisine
 * [Aller à l'armurerie] -> armurerie
 * [Parler au garde dans l'entrée] -> garde
 * [Atteindre la porte et {tenter de partir|partir d'ici|commencer l'aventure|enfin partir}] -> aventure
 
= cuisine
Vous arrivez dans une sublime cuisine. Des pains de toutes sortes, des pâtisseries et des tartes pour tous les goûts vous nourissent les yeux. Mais vous visez quelque chose de bien plus important.
    * [Boire un café] 
        Vous profitez de cette boisson chaude qui sans nul doute saura vous sauvez de situations compliquées grâces à ces propriétés vivifiantes.
        ~ coffee = true
        -> main_room
    * [Rebrousser chemin] -> main_room
= armurerie
Vous poussez la grande porte et trouvez là bon nombre de haches, d'épées et de serpes variées, de quoi découper un beau jambon, et peut être même un ennemi !
    * [Prendre la première épée]
        {coffee == true:
            Après une minutieuse revue de toutes les armes de la salle, vous prenez la première épée devant vous. Elle brille, elle doit probablement fonctionner.
            ~ arme = "épée"
            - else:
            Les yeux encore collés, vous manquez de vous faire masser les cervicales par une hallebarde qui vous regardait de travers.
            Pour éviter tout risque, vous attraper une dague misérable et rouillée gentillement posée au sol.
            ~ arme = "dague"
        
        }
        -> main_room
    * [Explorer la pièce] -> deeper
    * [Sortir humblement] -> main_room
= deeper
    En poussant tout l'amoncellement d'armes défectueuses, <>
    {coffee==true:
    vous trouvez, non sans peine, un magnifique coffre couvert d'or et dont la serrure semble flambant neuve. 
    {not garde:Un cadenas fermement accroché vous empêche de profiter de ce que cette trouvaille contient.}
    {garde: 
    Heureusement que cette sublime clé était la bonne, vous voilà accompagné d'un arc en argent et de flèches elfiques d'une valeur très certainement inestimable ! 
    ~ arme = "arc"
    }
    - else:vous ne remarquez absolument rien.
    }
    -> main_room
    
= garde
garde:Hé minot ! Je viens d'apprendre un grand secret, je te le répète, si TOI tu me promet de ne le répéter à personne
    * [Promettre] 
        garde:J'ai trouvé cette clé bien brillante dans mon sandwich ce matin. Je te la laisse, mais surtout ne répète pas a Hilda que j'ai mangé aujourd'hui !
        -> main_room
    * [Faire mine de ne rien avoir entendu] -> main_room
=== aventure ===
 Vous poussez la grande porte et vu le froid qu'il fait vous regrettez de ne pas avoir pris une petite laine en partant. Vous gravissez avec aisance cette montagne habillée d'un voile blanc.
 C'est une fois en haut que vous la voyez : la gemme de pouvoir ! Elle vous éblouirait presque. Mais un rugissement gronde.
Quelqu'un vous observe.
 Isidor, le dragon de compagnie de Smordok ne se fait pas atteindre et ramène son corps rond et d'un vert éclatant prêt de vous.
 {arme:
 - "":Et là, vous vous souvenez que dans une profonde maladresse, vous avez oublié de prendre une arme au château. Quelle tragégie.
 - "dague":Il vole, mais pas vous. Votre arme ne fera jamais le poids face à ce monstre terrifiant. Vous disparaissez dans les montagnes et plus jamais personne ne vous revi sur ces terres.
 - "épée":Votre lame est resplendissante, mais que vaut le style face à un tel énergumène ? Impossible de l'atteindre à cette hauteur, vous décidez d'engager tout votre courage dans la bataille et lancez votre glaive sur l'animal volant.
C'est raté. Dommage. Vous retournez au château bredouille et serez certainement privé de dessert.
- else:Le vaillant volatile en voyant votre valeureuse arme est encore sous le choc. Il sait très bien qu'il vit ses derniers instants.
Ce n'est qu'après 200 flèches et 199 tirs manqués que vous revenez avec toute la gloire qui vous caractérise. La sorcière vous déteste finalement un peu moins que ce matin.
}

Et c'est déjà la fin de l'aventure !
    -> END
