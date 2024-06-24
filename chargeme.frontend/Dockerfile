FROM node:18.20.2-alpine

# set working directory
WORKDIR /app
COPY package*.json .

RUN npm install --no-audit

COPY . .

RUN npm run build

EXPOSE 3000

CMD [ "npm", "run", "preview" ]