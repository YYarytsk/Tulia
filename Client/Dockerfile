#Inital build stage
FROM node:14-alpine as build
WORKDIR /app
COPY ["package.json", "package-lock.json*", "./"]
#Restore
RUN npm ci
COPY [ ".", "./" ]
RUN npx ng build

# Second Stage
FROM nginx:latest
WORKDIR /usr/share/nginx/html
#Remove default page
RUN rm -rf *
#Deep Link configuring
COPY nginx-custom.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/* ./

