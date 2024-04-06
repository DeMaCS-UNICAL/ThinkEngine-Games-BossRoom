friend(A,B,C,D,E):- friends_id(_,_,X,A), friends_is_alive(_,_,X,B), friends_health(_,_,X,C), friend_pos_x(_,_,X,D), friend_pos_z(_,_,X,E).

dead(I,X,Z):- friends_id(_,_,I,D), friends_health(_,_,I,C), C<=0, friend_pos_x(_,_,I,X), friend_pos_z(_,_,I,Z).

dist_x(I,X):- dead(_,A,_), player_x(_,_,B), X=A-B, my_id(_,_,I).
dist_z(I,Z):- dead(_,_,A), player_z(_,_,B), Z=A-B, my_id(_,_,I).

dist_x(I,X):- dead(Q,A,_), friend_pos_x(_,_,I,B), Q!=I, X=A-B.
dist_z(I,Z):- dead(Q,_,A), friend_pos_z(_,_,I,B), Q!=I, Z=A-B.

sqr(I,A):- dist_x(I,X), dist_z(I,Z), Q=X*X, W=Z*Z, A=Q+W.

closest(I):- #min{A: sqr(I,A)}=X, sqr(I,X).

applyAction(0, "player_revive"):- closest(I), my_id(_,_,I).
actionArgument(0, "friend_id", I):- applyAction(0, "player_revive"), dead(D,_,_), friends_id(_,_,D,I).
actionArgument(0, "my_id", I):- applyAction(0, "player_revive"), my_id(_,_,I).
actionArgument(0, "friend_index", I):- applyAction(0, "player_revive"), dead(I,_,_).
%, friends_id(_,_,I,D).