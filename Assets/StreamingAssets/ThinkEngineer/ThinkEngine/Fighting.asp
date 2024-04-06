
%enemy(0,X,Z):- entity_tag(_, _, 0, "Imp"), entity_x(_, _, 0, X), entity_z(_, _, 0, Z).

%calcola la distanza tra il player e l'Imp
%dist_x(I, X):- enemy(I, A, _),  player_x(_, _, B), X=A-B.
%dist_z(I, Z):- enemy(I, _, A),  player_z(_, _, B), Z=A-B.

%sqr(I, A):- dist_x(I, X), dist_z(I, Z), Q=X*X, W=Z*Z, A=Q+W.

%target(I):- sqr(I,A)
 
%atomo che calcola 1/4 della distanza da percorre per essere davanti al nemico
%rotate_x(X):- dist_x(A), X=A/4.
%rotate_z(Z):- dist_z(A), Z=A/4.

%%posizione globale effettiva sulla mappa
%dest_x(X):- rotate_x(A), player_x(_, _, B), X=B+A.
%dest_z(Z):- rotate_z(A), player_z(_, _, B), Z=B+A.

%applyAction(0, "Move"):-enemy(0, X, Y).
%actionArgument(0, "x", X):- applyAction(0, "Move"), dest_x(X).
%actionArgument(0, "z", Z):- applyAction(0, "Move"), dest_z(Z).
%actionArgument(0, "id", I):- applyAction(0, "Move"), info_m_id(_,_,I).
%actionArgument(0, "x_now", X):- player_x(_,_,X), applyAction(0,"Move").
%actionArgument(0, "z_now", Z):- player_z(_,_,Z), applyAction(0,"Move").


%enemy(0):-entity_tag(_, _, 0, "Imp").

melee(1):- tipo(_, _,0).
melee(1):- tipo(_, _,3).
ranged(1):- tipo(_, _,1).
ranged(1):- tipo(_, _,2).

need_heal(X):- friends_health(_,_,X,H), H<50, H>0.

applyAction(0, "player_attack"):- entity_tag(_, _, 0, _), ho_un_target(_, _, 0), melee(1).
applyAction(0, "player_attack"):- entity_tag(_, _, 0, _), tipo(_,_,1).
applyAction(0, "player_attack"):- entity_tag(_, _, 0, _), tipo(_,_,2), #count{X: need_heal(X)}=0.
actionArgument(0, "enemy_index", 0):- applyAction(0, "player_attack").
actionArgument(0, "id", I):- applyAction(0, "player_attack"), my_id(_,_,I).
actionArgument(0, "type", T):- applyAction(0, "player_attack"), tipo(_, _,T).