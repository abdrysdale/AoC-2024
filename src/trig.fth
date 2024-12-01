\ Shameless stolen Gareth Wilson :
\ https://www.hpmuseum.org/forum/thread-18123-post-157971.html#pid157971
\ Not needed for gforth but copied incase I ever need this for simpler forth.
\ All comments are Gareth's not mine.

: U*/_RND       ( U1 U2 U3 -- U1*U2/U3 )    \ Fractional part of answer is rounded in, not truncated.
   >R  UM*
   R@  -1 SHIFT  0  D+
   R>  UM/MOD  NIP      ;


\ cos45 below uses the 1st 4 terms of the infinite series, but I changed one of the coefficients to improve the accuracy.
\ The 8002 started as 7FFF.  The way it is below, the max error I've seen is 1 lsb high.  Usually it's right on though.


7FFF  CONSTANT  7FFF

: cos45   ( rad•28BE -- cos•$7FFF )     \ Input (X) is 0 (0°) up to $2000 (45°).
   7FFF 28BE U*/_RND     \ First, scale the input so 1 radian is 7FFF instead of 28BE.  Now max (45°) is $6488.
   DUP  7FFF U*/_RND     \ ^ 7FFF•X²                            TOS range is     0 for X=0 to $4EF6 for X=45°.
   DUP 168 + 2D0 /       \ ^    "    same/6!         (6!=$2D0.) TOS range is     0 for X=0 to   $1C for X=45°.
         555 SWAP -      \ ^    "    7FFF•(1/4!-X²/6!)          TOS range is  $555 for X=0 to  $539 for X=45°.
   OVER 7FFF U*/_RND     \ ^    "    7FFF•X²(1/4!-X²/6!)        TOS range is     0 for X=0 to  $339 for X=45°.
        4000 SWAP -      \ ^    "    7FFF•(1/2!-X²(1/4!-X²/6!)  TOS range is $4000 for X=0 to $3CC7 for X=45°.
        8002 U*/_RND     \ ^ 7FFF•X²(1/2!-X²(1/4!-X²/6!)        TOS range is     0 for X=0 to $257D for X=45°.
        7FFF SWAP -   ;  \ ^ 7FFF•(1-X²(1/2!-X²(1/4!-X²/6!)     TOS range is $7FFF for X=0 to $5A82 for X=45°.


\ sin45 below uses the 1st 3 terms of the infinite series, but I changed a couple of the coefficients by 1 to improve the
\ accuracy.  The 1556 started as 1555, and the final 8000 was 7FFF.  The way it is below, the max error I've seen is 1 lsb
\ high, as long as the input does not exceed 45°.  Even a little above 45°, the accuracy is lost very quickly!


: sin45   ( rad•28BE -- sin•$7FFF )     \ Input (X) is 0 (0°) up to $2000 (45°).
   7FFF 28BE U*/_RND     \ First, scale the input so 1 radian is 7FFF instead of 28BE.  Now max (45°) is $6488.
   DUP                   \ ^ 7FFF•X  7FFF•X                     Keep a copy of unsquared scaled input for use later.
   DUP  7FFF U*/_RND     \ ^    "    7FFF•X²                           TOS range is     0 for X+0 to $4EF6 for X=45°.
   DUP 3C + 78 /         \ ^    "    7FFF•X²  7FFF•X²/5!    (5!=$78.)  TOS range is     0 for X=0 to   $A8 for X=45°.
        1556 SWAP -      \ ^    "    7FFF•X²  7FFF•(1/3!-X²/5!)        TOS range is $1556 for X=0 to $14AE for X=45°.
        7FFF U*/_RND     \ ^    "    7FFF•X²(1/3!-X²/5!)               TOS range is     0 for X=0 to  $CC1 for X=45°.
        7FFF SWAP -      \ ^    "    7FFF•(1-X²(1/3!-X²/5!))           TOS range is $7FFF for X=0 to $733E for X=45°.
        8000 U*/_RND  ;  \ ^ 7FFF•X(1-X²(1/3!-X²/5!)                   TOS range is     0 for X=0 to $5A82 for X=45°.


: sin90   ( rad•28BE -- sin•$7FFF )     \ Input is 0 (0°) up to $4000 (90°).
   DUP  2000  >                         \ Is it over 45°?
   IF   4000 SWAP - cos45  EXIT         \ If so, just take the cosine of the opposite angle.
   THEN sin45   ;                       \ Otherwise just go straight to the sin word above.


: cos90   ( rad•28BE -- cos•$7FFF )     \ Input is 0 (0°) up to $4000 (90°).  I don't remember why it's here, since it's not used
   DUP  2000  >                         \ Is it over 45°?                                                            in COS below.
   IF   4000 SWAP - sin45  EXIT         \ If so, just take the cosine of the opposite angle.
   THEN cos45   ;                       \ Otherwise just go straight to the cos word above.


: SIN      ( rad•28BE -- sin•$7FFF )
   DUP  3FFF AND                        \ ^ RADs HW_FAR_N2_QUADRANT
   OVER 4000 AND                        \ ^  "          "       BACKWARDS?
   IF   4000 SWAP - THEN  sin90         \ ^  "        90°_SIN
   SWAP 0<                              \ ^ 90°_SIN  RADs
   IF NEGATE THEN       ;               \ ^ SIN


: COS    4000 + SIN              ;    ( RAD -- COS )       \ Good for any quadrant.
: TAN       DUP SIN   SWAP COS   ;    ( RAD -- SIN COS )   \ For any quadrant