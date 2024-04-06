friend(A,B,C,D,E):- friends_id(_,_,X,A), friends_is_alive(_,_,X,B), friends_health(_,_,X,C), friend_pos_x(_,_,X,D), friend_pos_z(_,_,X,E).

im_healer(1):- tipo(_,_,2).
%need_heal(I):- friend(I,1,X), X<50.
%need_heal(I):- my_id(_,_,I), my_health(_,_,X), X<50.

need_heal(I,X,Z):- friend(I,1,A,X,Z), A<=50, A>0.
need_heal(I,X,Z):- my_id(_,_,I), my_health(_,_,A), A<=50, A>0, player_x(_,_,X), player_z(_,_,Z).

dist_x(I, X):- need_heal(I,A,_), player_x(_,_,B), X=A-B.
dist_z(I, Z):- need_heal(I,_,A), player_z(_,_,B), Z=A-B.

sqr(I,A):- dist_x(I, X), dist_z(I, Z), Q=X*X, W=Z*Z, A=Q+W.

closest(I):- #min{A: sqr(I,A)}=X, sqr(I,X).

applyAction(0, "player_heal"):- im_healer(1), closest(I).
actionArgument(0, "friend_id", I):- applyAction(0, "player_heal"), closest(I).
actionArgument(0, "my_id", I):- applyAction(0, "player_heal"), my_id(_,_,I).
