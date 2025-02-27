# CafetClash
CafetClash est un projet scolaire r�alis� dans le cadre du cours d'algorithme sur le th�me des algorithmes de matching.
Le but de ce projet est de d'avoir un logiciel permettant de g�n�rer et g�rer des matchs entre des joueurs de jeux dans la cafet du batiment.
Le projet est r�alis� en C# et utilise Windows Forms pour l'interface graphique.

## L'algorithme de matching
L'algorithme de matching se base sur un syst�me de point �lo attribu� au joueurs et calcul� en fonction de leur performance lors des matchs.
**
Il fonctionne de la sorte :
    - On doit choisir un joueur de d�part
    - On doit choisir un type de jeu
    - Par rapport � la difficulte choisie l'algorithme s'addapt�
        - Pour un Match Facile :
            - On prend les joueurs ayant moins de 100 points d'�lo compar� au joueur de d�part
            - On prend les joueurs ayant un �lo similaire (entre -100 et +100) mais qui on un ratio de victoire inf�rieur � 50%
        - Pour un Match Moyen :
            - On prend les joueurs ayant un �lo similaire (entre -100 et +100)
            - On prend les joueurs ayant moins de 200 �lo mais qui ont un ratio de victoire sup�rieur � 50%
            - On prend les joueurs ayant plus de 200 �lo mais qui ont un ratio de victoire inf�rieur � 50%
        - Pour un Match Difficile :
            - On prend les joueurs ayant plus de 100 �lo compar� au joueur de d�part
            - On prend les joueurs ayant un �lo similaire (entre -100 et +100) mais qui on un ratio de victoire sup�rieur � 50%
        - Pour choisir un joueurs dans la liste d'opposant potentiel :
            - On tire un nombre entre 1 et 100 (si on est pas dans un match �quilibr�)
                - Si le nombre est inf�rieur � 81 (80% de chance) on prend les joueurs ayant un delta d'�lo inf�rieur � 300 
                et un ratio de victoire sup�rieur � 50% pour avoir un match facile �quilibr�.
                - Si le nombre est entre 81 et 90 (10% de chance) on prend les joueurs ayant un delta d'�lo sup�rieur � 300
                et inf�rieur � 500 pour avoir un match tr�s facile.
                - Si le nombre est entre 91 et 100 (10% de chance) on prend les joueurs ayant un delta d'�lo sup�rieur � 500
            - On choisi un joueur al�atoirement dans la nouvelle liste d'opposant potentiel
**
Le but de l'algo est de faire en sorte que les joueurs aient des matchs �quilibr�s et que les joueurs ayant un �lo tr�s diff�rent ne se rencontrent pas
tout en permetant d'avoir des match difficile, �quilibr� et facile.