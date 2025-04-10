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
                        string(credentialsId: 'CLIENTID', variable: 'CLIENTID'),
                        string(credentialsId: 'APIKEY', variable: 'APIKEY'),
                        string(credentialsId: 'CHECKSUM', variable: 'CHECKSUM'),
                        string(credentialsId: 'EMAIL_SMTP_SERVER', variable: 'EMAIL_SMTP_SERVER'),
                        string(credentialsId: 'EMAIL_PORT', variable: 'EMAIL_PORT'),
                        string(credentialsId: 'EMAIL_SENDER_EMAIL', variable: 'EMAIL_SENDER_EMAIL'),
                        string(credentialsId: 'EMAIL_USERNAME', variable: 'EMAIL_USERNAME'),
                        string(credentialsId: 'EMAIL_PASSWORD', variable: 'EMAIL_PASSWORD')
                        
                    ]) {
                        env.CLOUDINARY_URL = "${CLOUDINARY_URL}"
                        env.REDIS_URL = "${REDIS_URL}"
                        env.REDIS_HOST = "${REDIS_HOST}"
                        env.REDIS_PORT = "${REDIS_PORT}"
                        env.REDIS_USER = "${REDIS_USER}"
                        env.REDIS_PASSWORD = "${REDIS_PASSWORD}"
                        env.ABLY_API_KEY = "${ABLY_API_KEY}"
                        env.CLIENTID = "${CLIENTID}"
                        env.APIKEY = "${APIKEY}"
                        env.CHECKSUM = "${CHECKSUM}"
                        env.CHECKSUM = "${EMAIL_SMTP_SERVER}"
                        env.CHECKSUM = "${EMAIL_PORT}"
                        env.CHECKSUM = "${EMAIL_SENDER_EMAIL}"
                        env.CHECKSUM = "${EMAIL_USERNAME}"
                        env.CHECKSUM = "${EMAIL_PASSWORD}"
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
                sh 'docker container run -d --name blcapstone -p 8080:8080 -p 8081:8081 ' +
                   "-e CLOUDINARY_URL=${env.CLOUDINARY_URL} " +
                   "-e REDIS_URL=${env.REDIS_URL} " +
                   "-e REDIS_HOST=${env.REDIS_HOST} " +
                   "-e REDIS_PORT=${env.REDIS_PORT} " +
                   "-e REDIS_USER=${env.REDIS_USER} " + 
                   "-e REDIS_PASSWORD=${env.REDIS_PASSWORD} " +
                   "-e ABLY_API_KEY=${env.ABLY_API_KEY} " +
                    "-e CLIENTID=${env.CLIENTID} " +
                    "-e APIKEY=${env.APIKEY} " +
                    "-e CHECKSUM=${env.CHECKSUM} " +
                    "-e CHECKSUM=${env.EMAIL_SMTP_SERVER} " +
                    "-e CHECKSUM=${env.EMAIL_PORT} " +
                    "-e CHECKSUM=${env.EMAIL_SENDER_EMAIL} " +
                    "-e CHECKSUM=${env.EMAIL_USERNAME} " +
                    "-e CHECKSUM=${env.EMAIL_PASSWORD} " +
                   'tuanhuu3264/blcapstone'
            }
        }
    }

    post {
        always {
            cleanWs()
            echo 'Pipeline is success!'
            emailext(subject: "Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]' ",
            body: "Job '<${env.BUILD_URL}>' Success.",
            to: 'binhbeopro1122@gmail.com')
        }
    }
}
