# pull official base image
FROM node:latest

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY . . 

RUN npm install 
RUN npm install react-scripts@3.4.1 -g --silent

# add app
#COPY . ./

EXPOSE 8080

# start app
CMD ["npm", "start"]