%Diamo per scontato che il player conosca la mappa di gioco.
applyAction(0,"Player_moviment").
actionArgument(0, "x", -300):-player_x(_,_,X), applyAction(0,"Player_moviment").
actionArgument(0, "z", -2600):-player_z(_,_,Z), applyAction(0,"Player_moviment").
actionArgument(0, "id", I):-info_m_id(_,_,I), applyAction(0,"Player_moviment").
actionArgument(0, "enemy_near", false):-applyAction(0,"Player_moviment"), #count{X: entity_tag(_,_,X,"Imp")}=0.
actionArgument(0, "enemy_near", true):- applyAction(0,"Player_moviment"), #count{X: entity_tag(_,_,X,"Imp")}>0.

%enemy(X,Z):- entity_x(_,_,0,X), entity_z(_,_,0,Z).
%direction_x(X):- enemy(A,_), player_x(_,_,Q), X=A-Q.
%direction_z(Z):- enemy(_,B), player_z(_,_,Q), Z=B-Q.
%stop(0):- direction_x(X), X<=100, direction_z(Z), Z<=100.
%orientation_x(X):- direction_x(A), X=A/3.
%orientation_z(Z):- direction_z(B), Z=B/3.
%moveto_x(X):- orientation_x(A), player_x(_,_,B), X=B+A.
%moveto_z(Z):- orientation_z(A), player_z(_,_,B), Z=B+A.
%moveto(A,B):- orientation_x(X), orientation_z(Z), player_x(_,_,A).
%moveto(X,Z):- direction(A,B), X=A/4, Z=B/4.

%applyAction(1,"Player_moviment"):- #count{X,Z: enemy(X,Z)}>0.
%actionArgument(1, "x", X):- moveto_x(X), applyAction(1,"Player_moviment"), not stop(0).
%actionArgument(1, "z", Z):- moveto_z(Z), applyAction(1,"Player_moviment"), not stop(0).

%actionArgument(1, "x", X):- player_x(_,_,X), applyAction(1,"Player_moviment"), stop(0).
%actionArgument(1, "z", Z):- player_z(_,_,Z), applyAction(1,"Player_moviment"), stop(0).

%actionArgument(1, "id", I):-info_m_id(_,_,I), applyAction(1,"Player_moviment").
%actionArgument(1, "enemy_near", true).
%actionArgument(1, "fight", true).