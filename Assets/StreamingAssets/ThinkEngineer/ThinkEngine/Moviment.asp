zone_1(1):- player_x(_,_,X), X>=-800, X<=1500, player_z(_,_,Z), Z<=900, Z>=-2100.
zone_2(1):- player_x(_,_,X), X>=-1600, X<=2200, player_z(_,_,Z), Z<=-2100, Z>=-3000.
zone_3(1):- player_x(_,_,X), X>2200, X<=5000, player_z(_,_,Z), Z>=-2800, Z<=-1400.
zone_4(1):- player_x(_,_,X), X>2000, X<=4700, player_z(_,_,Z), Z>-1400, Z<=1200.
zone_5(1):- player_x(_,_,X), X>1600, X<=6000, player_z(_,_,Z), Z>1200, Z<=4600.
zone_6(1):- player_x(_,_,X), X>6000, X<=13500, player_z(_,_,Z), Z>0, Z<=7500.

dir_x(X):- zone_1(1), player_x(_,_,X).
dir_z(Z):- zone_1(1), player_z(_,_,A), Z=A-500.

dir_x(X):- zone_2(1), player_x(_,_,A), X=A+500.
dir_z(Z):- zone_2(1), player_z(_,_,Z).

dir_x(X):- zone_3(1), player_x(_,_,A), X=A+250.
dir_z(Z):- zone_3(1), player_z(_,_,A), Z=A+500.

dir_x(X):- zone_4(1), player_x(_,_,X).
dir_z(Z):- zone_4(1), player_z(_,_,A), Z=A+500.

dir_x(X):- zone_5(1), player_x(_,_,A), X=A+500.
dir_z(Z):- zone_5(1), player_z(_,_,A), Z=A+500.

dir_x(11700):- zone_6(1).
dir_z(3700):- zone_6(1).

applyAction(0,"Move"):-my_current_action(_,_,0), #count{X: entity_tag(_, _, X, _)}=0.
actionArgument(0, "x", X):- applyAction(0,"Move"), dir_x(X).
actionArgument(0, "z", Z):- applyAction(0,"Move"), dir_z(Z).
actionArgument(0, "id", I):- my_id(_,_,I), applyAction(0,"Move").
actionArgument(0, "x_now", X):- player_x(_,_,X), applyAction(0,"Move").
actionArgument(0, "z_now", Z):- player_z(_,_,Z), applyAction(0,"Move").