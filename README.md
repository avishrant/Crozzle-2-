# Crozzle
(To be improved solution) for the game Crozzle.

### Introduction
I couldn’t find reference to this particular problem anywhere (except from a programmer of the month competition in 1995). This is very similar to solving a crossword however there are differences.

Note: The following is meant to provide insight in layman's terms into the problem that is a crossword solving algorithm.

Note: The following is based on a game called a Crozzle. The rules for the game Crozzle can be googled to be understood.

Note: Specific conditions apply to this solution please refer to the rules.

Here I am not going to provide any mathematical notation or even a proof that I based my approach on, because I didn’t have one!
The solution presented here was not the one I finally came up with, it is simple and stripped down for maximum readability and is intended to be easily understandable. It still however generates a good solution that can be easily adapted and expanded. 
The purpose here is to give you in laymen's terms a means to approach the problem with logic and give you a good starting point to create your solution. 

Do not however get lazy and JUST use this code I went to the trouble of putting this explanation together because I want you to find a BETTER solution!

Don’t be afraid of scrapping all of your code and starting from scratch here this problem has no best solution (will explain this later) and your probably not going to come up with it. 
Instead get the best solution you can dream up and the best way to do that is to build off others work!

Anyway, here’s a Crozzle solution.



### The Problem
Ok so first to get started, there is no real best approach to this problem. You might be thinking what do you mean theres no best solution? Well this particular problem is a NP-Hard problem. Google NP-Hard (some work is involved here).
Now at first glance, obviously the only way to ensure we have the best possible solution is to check every possible combination. That’s how we know were right we try everything, RIGHT?

WRONG!

If you aren’t aware yet Ill burst your bubble. You CANT use brute force! 
As this problem scales there are simply to many conbinations to check (Google around if you don’t believe me!).
So now that we’ve established brute force is out the window we need to come up with some solid heuristics!

What are heuristics? You might be asking. A computing definition defines it as "proceeding to a solution by trial and error or by rules that are only loosely defined." 
Simply put, pick some rules that increase the likelihood of a good scoring Crozzle!
What rules you might be thinking? Well try making them up!
That’s it make some assumptions about the problem and try them!

Keep in mind the heuristics you come up with apply to the rules and constraints of your game, here’s some food for thought.
•	Words with the most common occurring characters.
•	Long words (if you’re trying to maximize intersections).
•	Starting with a short word.
•	Starting with a long word.

Simple list but you get the point.

All im trying to illustrate here is that through trial and error you have to gradually improve your solution, which all starts with something you make up right now!



### Rules for this crozzle
1. A horizontal word must intersect 1 or 2 vertical words.
2. A vertical word must intersect 1 or 2 horizontal words. 
3. One point for each letter within the Crozzle. 
4. A horizontal word cannot touch any other horizontal word. Meaning there must be at least one grid space between a horizontal word and any other horizontal word (Refer to picture).
5. A vertical word cannot touch any other vertical word. That is, there must be at least one grid space between a vertical word and any other vertical word (Refer to picture).  

![Rule 4, 5 example.](https://i.imgur.com/cTNCj5l.png)


### How the program solves the problem
The solution is by no means a best answer, it is a greedy algorithm.
It iterates over every possible placement, horizontal and vertical for the first word placement (createFirstWordPlacement) trying every word in the wordlist for those particular co-ordinates. It then places as many word connections as many words as possible (createConnectingWordPlacement) to the previous word. It then caches the highescore and continues to generate crozzle combinations until no more are found.
Because this is a greedy algorithm it doesn’t take an excesively long time to complete execution and find a solution. However youll notice execution does tie up the main worker thread and the program will freeze when in execution. I didn’t putt the crozzle generation on a separate thread as a means to keeep the code simple.



### What you should do
You should either attempt to optimise this code to get a more optimal solution, or you could re-write the code entirely for something even better. If your re-writing the solution note the (CrozzleGrid) class was designed to be as portable as possible and the code is highly custimasable and re-usable to suit your needs.

The methods:
- checkHorizontalWordFitsGrid
- checkVerticalWordFitsGrid
- checkTouchingHorizontalWords
- checkTouchingVerticalWords
- checkHorizontalWordPath
- checkGridIntersections

Are working condition checking methods.
They can and should be re-used to save you time working out how to test for conditions and allow you more time to optimise your solution!



### Conclusion
You should focus on playing around with the (methodology) of generation. The “word checking methods” within the CrozzleGrid class are completely portable and can be re-used allowing you a lot more time to focus on your solution!

Happy Coding!
