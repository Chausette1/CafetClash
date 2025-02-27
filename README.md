# CafetClash
CafetClash est un projet scolaire réalisé dans le cadre du cours d'algorithme sur le thème des algorithmes de matching.
Le but de ce projet est de d'avoir un logiciel permettant de générer et gérer des matchs entre des joueurs de jeux dans la cafet du batiment.
Le projet est réalisé en C# et utilise Windows Forms pour l'interface graphique.

## L'algorithme de matching
L'algorithme de matching se base sur un système de point élo attribué au joueurs et calculé en fonction de leur performance lors des matchs.

Il fonctionne de la sorte :

    - On doit choisir un joueur de départ
    - On doit choisir un type de jeu
    - Par rapport à la difficulte choisie l'algorithme s'addapté
        - Pour un Match Facile :
            - On prend les joueurs ayant moins de 100 points d'élo comparé au joueur de départ
            - On prend les joueurs ayant un élo similaire (entre -100 et +100) mais qui on un ratio de victoire inférieur à 50%
        - Pour un Match Moyen :
            - On prend les joueurs ayant un élo similaire (entre -100 et +100)
            - On prend les joueurs ayant moins de 200 élo mais qui ont un ratio de victoire supérieur à 50%
            - On prend les joueurs ayant plus de 200 élo mais qui ont un ratio de victoire inférieur à 50%
        - Pour un Match Difficile :
            - On prend les joueurs ayant plus de 100 élo comparé au joueur de départ
            - On prend les joueurs ayant un élo similaire (entre -100 et +100) mais qui on un ratio de victoire supérieur à 50%
        - Pour choisir un joueurs dans la liste d'opposant potentiel :
            - On tire un nombre entre 1 et 100 (si on est pas dans un match équilibré)
                - Si le nombre est inférieur à 81 (80% de chance) on prend les joueurs ayant un delta d'élo inférieur à 300 
                et un ratio de victoire supérieur à 50% pour avoir un match facile équilibré.
                - Si le nombre est entre 81 et 90 (10% de chance) on prend les joueurs ayant un delta d'élo supérieur à 300
                et inférieur à 500 pour avoir un match très facile.
                - Si le nombre est entre 91 et 100 (10% de chance) on prend les joueurs ayant un delta d'élo supérieur à 500
            - On choisi un joueur aléatoirement dans la nouvelle liste d'opposant potentiel

Le but de l'algo est de faire en sorte que les joueurs aient des matchs équilibrés et que les joueurs ayant un élo très différent ne se rencontrent pas
tout en permetant d'avoir des match difficile, équilibré et facile.
