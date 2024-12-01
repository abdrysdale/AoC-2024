\ utility words and constants

\ *** top stack manipulation *** /
: 2rot ( a b c -- c a b) rot rot ;

\ *** whole stack manipulation *** /
\ clears the stack
: .c ( n1 ... nn -- ) depth 0 do drop loop ;
\ views the stack with indexes
: .v ( -- )
    depth 0= if
        cr ." stack empty."
    else
        depth 0 do
            cr i . ." : " i pick .
        loop
    then ;
\ views the stack with the indexes if pick was run next.
: .p ( -- )
    depth 0= if
        cr ." stack empty."
    else
        depth 0 do
            i 0= if
                cr ." ** " i pick . ." pick ( " dup 1 + pick . ." ) **"
            else
                cr i 1 - . ." : " i pick .
            then
        loop
    then ;
\ views the stack with debugging information
: .d ( -- ) cr ." -- " r@ . ." --" cr .v ;

\ *** string *** /
: nospace ( n -- ) 0 <# #s #> type ;

\ *** date and time *** /
: now ( -- ) time&date 2 0 do nospace ." /" loop . 2 0 do nospace ." :" loop . ;
