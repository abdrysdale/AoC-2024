\ Matrix related functions
\ Note: Matrices must be in the form [ M N a1,1 ... a1,N ... aM,1 ... aM,N ]

include math.fs

: shape= ( addr1 addr2 -- n ) { addr1 addr2 }
    addr1 @ addr2 @ = addr1 1 get addr2 1 get = and ;

: exit-shape/= ( addr1 addr2 -- )
    shape= invert if ." Matrix shapes not equal" exit then ;

: m+ ( addr1 addr2 addr3 -- addr3 ) { addr1 addr2 addr3 }
    addr1 addr2 exit-shape/= 2 0 DO addr1 i get addr3 i put LOOP
    addr1 @ addr1 1 get * 2 + 2 DO addr1 i get addr2 i get + addr3 i put LOOP ;

\ *** Tests *** /
create test-no 0 ,
: test-msg ( n -- )
    cr if test-no ? ." Passed." else test-no ? ." Failed." then 1 test-no +! ;
create test-m1 2 , 3 , 4 , 5 , 6 , 40 , 50 , 60 ,
create test-m2 2 , 3 , 7 , 8 , 9 , 70 , 80 , 90 ,
create test-m3 3 , 2 , 4 , 5 , 40 , 50 , 400 , 500 ,
: test-shape= ( -- )
    test-m1 test-m2 shape= test-msg
    test-m1 test-m3 shape= invert test-msg ;
create test-m1+m2 2 , 3 , 11 , 13 , 15 , 110 , 130 , 150 ,
variable test-m1+m2-o 2 3 * 1 + cells allot
: test-m+ ( -- )
    test-m1 test-m2 test-m1+m2-o m+
    -1 8 2 DO test-m1+m2 i get test-m1+m2-o i get = and LOOP test-msg ;

: run-tests ( -- ) test-shape= test-m+ ;
