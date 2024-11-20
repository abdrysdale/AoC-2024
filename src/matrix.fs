\ Matrix related functions
\ Note: Matrices must be in the form [ M N a1,1 ... a1,N ... aM,1 ... aM,N ]

include math.fs

\ Convience function for matrix properties
: rows ( addr -- n ) 0 get ;
: cols ( addr -- n ) 1 get ;
: m-idx ( addr i j -- idx ) { addr i j } addr cols i * j + 2 + ;
: m-val ( addr i j -- val ) { addr i j } addr addr i j m-idx get ;
: m-size ( addr -- n ) { addr } addr rows addr cols * 2 + ;
: m-init ( addr m n -- ) { addr m n } m n * 1 + cells allot m addr 0 put n addr 1 put ;
variable .mvi
variable .mvj
: .mv ( addr -- ) { addr }
    cr .mvi 0! addr rows 0 do
        .mvj 0! addr cols 0 do
            addr .mvi @ .mvj @ m-val .n8
            .mvj ++
        loop cr .mvi ++
    loop ;
    

\ Shape checking
: shape= ( addr1 addr2 -- n ) { addr1 addr2 }
    addr1 rows addr2 rows = addr1 cols addr2 cols = and ;
: shape/= ( addr1 addr2 -- n ) shape= invert ;
: shape-conform ( addr1 addr2 -- n ) { addr1 addr2 } addr1 cols addr2 rows = ;
: shape-/conform ( addr1 addr2 -- n ) shape-conform invert ;
: m= ( addr1 addr2 -- n ) { addr1 addr2 }
    addr1 addr2 shape= addr1 rows addr1 cols * 2 + 2 do
        addr1 i get addr2 i get = and
    loop ;

\ Addition
: m+ ( addr1 addr2 addr3 -- addr3 ) { addr1 addr2 addr3 }
    addr1 addr2 shape/= if ." Shapes are not equal" exit then
    addr3 addr1 rows addr1 cols m-init
    addr3 m-size 2 do addr1 i get addr2 i get + addr3 i put loop addr3 ;
\ dot product
variable m.i  \ i, j and k change depending on the position of the loop
variable m.j  \ see loop-test for details, so using my own indexes
variable m.k
: m. ( addr1 addr2 addr3 -- addr3 ) { addr1 addr2 addr3 }
    addr1 addr2 shape-/conform if ." Matrices do not conform" exit then
    addr3 addr1 rows addr2 cols m-init
    m.i 0! addr3 rows 1 + 0 do
        m.j 0! addr3 cols 1 + 0 do
            m.k 0! 0 addr1 cols 0 do
                addr1 m.i @ m.k @ m-val addr2 m.k @ m.j @ m-val * +
                m.k ++
            loop addr3 addr3 m.i @ m.j @ m-idx put
            m.j ++
        loop m.i ++
    loop addr3 ;

\ *** tests *** /
: loop-test ( -- )
    3 0 do
        cr ." i" i .
        6 3 do
            cr ." i" i . ."  j" j . 
            9 6 do
                cr ." i" i . ."  j" j . ."  k" k .
            loop
        loop
    loop ;

create test-no 0 ,
: test-msg ( n -- )
    cr if test-no ? ." Passed." else test-no ? ." Failed." then 1 test-no +! ;
create test-m1 2 , 3 , 4 , 5 , 6 , 40 , 50 , 60 ,
create test-m2 2 , 3 , 7 , 8 , 9 , 70 , 80 , 90 ,
create test-m3 3 , 2 , 4 , 5 , 40 , 50 , 400 , 500 ,
: test-shape= ( -- ) 1 test-no !
    test-m1 test-m2 shape= test-msg
    test-m1 test-m3 shape= invert test-msg ;
create test-m1+m2 2 , 3 , 11 , 13 , 15 , 110 , 130 , 150 ,
variable test-m1+m2-o
: test-m+ ( -- ) 3 test-no !
    test-m1 test-m2 test-m1+m2-o m+
    test-m1+m2-o test-m1+m2 m= test-msg;

create test-m1.m3 2 , 2 , 2616 , 3270 , 26160 , 32700 ,
variable test-m1.m3-o
: test-m. ( -- ) 4 test-no !
    test-m1 test-m3 test-m1.m3-o m. test-m1.m3 m= test-msg ;

: run-tests ( -- ) test-shape= test-m+ test-m. ;
