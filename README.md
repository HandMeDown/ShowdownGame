# Poker Hand Showdown

Implement a library (in C#) which evaluates who are the winner(s) among several 5 card poker
hands. Note for this project that you only need to implement a subset of the regular poker
hands:

• Flush

• Three of a Kind

• One Pair

• High Card

## How your program should handle input and output

Input: Collection of players in the showdown.

• Player Name

• 5 Cards (each specifying the card rank and suit of the card)
Output: Collection of winning players (more than one in case of a tie)

Note: Please direct all output to the Console window (a GUI is not necessary for this task).

## Rules and Tie Breakers for those Subsets


Flush

A flush is any hand with five cards of the same suit. If two or more players hold a flush, the flush
with the highest card wins. If more than one player has the same strength high card, then the
strength of the second highest card held wins. This continues through the five highest cards in
the player's hands.

Three of a Kind

If more than one player holds three of a kind, then the higher value of the cards used to make
the three of kind determines the winner. If two or more players have the same three of a kind,
then a fourth card (and a fifth if necessary) can be used as kickers to determine the winner.

One Pair

If two or more players hold a single pair, then highest pair wins. If the pairs are of the same
value, the highest kicker card determines the winner. A second and even third kicker can be
used if necessary.

High Card

When no player has even a pair, then the highest card wins. When both players have identical
high cards, the next highest card wins, and so on until five cards have been used. In the
unusual circumstance that two players hold the identical five cards, the pot would be split.

For rules for other subsets of hands please see:
[adda52](https://www.adda52.com/poker/poker-rules/cash-game-rules/tie-breaker-rules)


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## Examples with Winners
...
Joe
8S, 8D, AD, QD, JH
Bob
AS, QS, 8S, 6S, 4S
Sally
4S, 4H, 3H, QC, 8C
Joe wins

Joe
QD, 8D, KD, 7D, 3D
Bob
AS, QS, 8S, 6S, 4S
Sally
4S, 4H, 3H, QC, 8C
Bob wins

Joe
3H, 5D, 9C, 9D, QH
Jen
5C, 7D, 9H, 9S, QS
Bob
2H, 2C, 5S, 10C, AH
Jen Wins

Joe
2H, 3D, 4C, 5D, 10H
Jen
5C, 7D, 8H, 9S, QD
Bob
2C, 4D, 5S, 10C, JH
Jen Wins
...

## Assumptions:
• Each players cards have already been dealt and will be provided via an input file.

• The input will be in the format Name on 1st line, and on the 2nd line 5 Cards separated by comma's - as per the examples.

• Data will always be given in the format valueSuit seperate by comma's eg. 8S, 8D, AD, QD, JH.

• Card will not be longer than 3 characters or less than 2.

• Kickers always start at highest card in each hand and descend together for each hand ie. always comparing nth with nth.

• There will always be 2+ players.

• No negative value cards will be input.

• Ace's are always high ie. value = 14.

• Cards may not be unique.

• Card values greater than 10 are always represented by face cards ie. J,Q,K,A.

• There will always be 5 cards in a given hand.

** There appears to be an error in the example data given.  I believe Example 1 should return 'Bob wins' as he is the only player with 
a Flush **
