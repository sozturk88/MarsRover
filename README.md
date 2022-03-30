
# 1. Create image
docker build -t roverimage .

# 2. Create container 
docker container create --name rovercontainer roverimage

# 3. Create container 
docker container start rovercontainer

# Optional : Create and run container 
docker run --name rovercontainer roverimage

# Optional : Attach The Container Running Process
docker container attach rovercontainer

