pipeline {
    agent any
    
    stages {
        stage('Retrieve Credentials') {
            steps {
                script {
                    withCredentials([
                        string(credentialsId: 'CLOUDINARY_URL', variable: 'CLOUDINARY_URL'),
                        string(credentialsId: 'REDIS_URL', variable: 'REDIS_URL'),
                        string(credentialsId: 'REDIS_HOST', variable: 'REDIS_HOST'),
                        string(credentialsId: 'REDIS_PORT', variable: 'REDIS_PORT'),
                        string(credentialsId: 'REDIS_USER', variable: 'REDIS_USER'),
                        string(credentialsId: 'REDIS_PASSWORD', variable: 'REDIS_PASSWORD'),
                        string(credentialsId: 'ABLY_API_KEY', variable: 'ABLY_API_KEY'),
                        
                    ]) {
                        env.CLOUDINARY_URL = "${CLOUDINARY_URL}"
                        env.REDIS_URL = "${REDIS_URL}"
                        env.REDIS_HOST = "${REDIS_HOST}"
                        env.REDIS_PORT = "${REDIS_PORT}"
                        env.REDIS_USER = "${REDIS_USER}"
                        env.REDIS_PASSWORD = "${REDIS_PASSWORD}"
                        env.ABLY_API_KEY = "${ABLY_API_KEY}"
                    }
                }
            }
        }
        stage('Packaging') {
            steps {
                sh 'docker build --pull --rm -f Dockerfile -t blcapstone:latest .'
            }
        }

        stage('Push to DockerHub') {
            steps {
                withDockerRegistry(credentialsId: 'dockerhub', url: 'https://index.docker.io/v1/') {
                    sh 'docker tag blcapstone:latest tuanhuu3264/blcapstone:latest'
                    sh 'docker push tuanhuu3264/blcapstone:latest'
                }
            }
        }

        stage('Deploy FE to DEV') {
            steps {
                echo 'Deploying and cleaning'
                echo "${env.DB_USER}"
                sh 'if [ $(docker ps -q -f name=blcapstone) ]; then docker container stop blcapstone; fi'
                sh 'echo y | docker system prune'
                sh 'docker container run -d --name blcapstone -p 6666:8080 -p 6667:8081 ' +
                   "-e CLOUDINARY_URL=${env.CLOUDINARY_URL} " +
                   "-e REDIS_URL=${env.REDIS_URL} " +
                   "-e REDIS_HOST=${env.REDIS_HOST} " +
                   "-e REDIS_PORT=${env.REDIS_PORT} " +
                   "-e REDIS_USER=${env.REDIS_USER} " + 
                   "-e REDIS_PASSWORD=${env.REDIS_PASSWORD} " +
                   "-e ABLY_API_KEY=${env.ABLY_API_KEY} " +
                   'tuanhuu3264/blcapstone'
            }
        }
    }

    post {
        always {
            cleanWs()
        }
    }
}
