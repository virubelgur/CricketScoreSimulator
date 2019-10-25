# CricketScoreSimulator
Random cricket score simulator based on weighted probability of each run scored for player

## Key Points
1. Domain Driven Design (DDD) <br/>
2. Reusable 
   - Entire game logic can be reused, only need to build front-end based on the available domain contract to support Mobile/Web application.<br/>
3. Configurable match settings
    - [MatchSettings] (ConsoleApp/appsettings.json) can be updated any time without re-builing application
      - Team/Players name, number of overs, players probality & everthing related to match can be configured.<br/>
4. .Net Core framework
    - Allows cross platfom for application <br/>
5. Cumulative Probability Distribution 
   - One of the realiable and fast alogorithm to find radom number by given probability<br/>
  
  ## How to build
  Dot net build with core 3 framework installed will work.
