# GlencoreWordGame.


Config
-- Please change logging file path in config to something appropriate. I tried to set it to the current base directory but it thought that was in the bin folder and i was unable to find a solution other than hard coding it. 
-- Other than that, build and run :)

Overall design goals
-- To be reasonably user friendly (e.g. give them feedback if they have done something wrong and to be easily readable).
-- To be configurable/to avoid hard coding as much as possible, both in terms of game options and class dependancies. 
-- To be basic in functionality but hard to break.
-- For any problems to be easily tracked via logs. 

Known Issues
-- If the the dictionaryAPI is unavailable for whatever reason, the game treats the users word as invalid. I'm aware this is not correct and would be the first thing i would change. 
-- I know the unit testing is far from complete. Given that i was already over the suggested time for the exercise i wanted them to be illustrative of my understanding rather than exhaustive.
-- The suggested API is somewhat unreliable, sometimes returning 404 for words that are definitely valid (e.g. HAVE) and often being slow to respond at all. Would switch this to another provider given more time. 

Next steps (other than those already mentioned)
-- Make it possible to play multiple rounds.
-- General UI improvments (include users name and high score).
-- Improve scoring to be more interesting. 
