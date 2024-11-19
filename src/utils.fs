\ Utility words and constants

\ *** Top stack manipulation *** /
: 2ROT ( a b c -- c a b) ROT ROT ;

\ *** Whole stack manipulation *** /
\ Clears the stack
: .C ( n1 ... nN -- ) DEPTH 0 DO DROP LOOP ;
\ Views the stack with indexes
: .V ( -- )
    DEPTH 0= IF
        CR ." Stack empty."
    ELSE
        DEPTH 0 DO
            CR i . ." : " i PICK .
        LOOP
    THEN ;
\ Views the stack with the indexes if PICK was run next.
: .P ( -- )
    DEPTH 0= IF
        CR ." Stack empty."
    ELSE
        DEPTH 0 DO
            i 0= IF
                CR ." ** " i PICK . ." PICK ( " DUP 1 + PICK . ." ) **"
            ELSE
                CR i 1 - . ." : " i PICK .
            THEN
        LOOP
    THEN ;
\ Views the stack with debugging information
: .D ( -- ) CR ." -- " R@ . ." --" CR .V ;

\ *** String *** /
: NOSPACE ( -- ) 0 <# #s #> TYPE ;

\ *** Date and Time *** /
: NOW ( -- ) time&date 2 0 DO NOSPACE ." /" LOOP . 2 0 DO NOSPACE ." :" LOOP . ;
