pipeline {
    agent any
    
    stages {
        stage('Retrieve Credentials') {
            steps {
                script {
                    withCredentials([
                        string(credentialsId: 'CLOUDINARY_URL', variable: 'CLOUDINARY_URL'),
                    ]) {
                        env.CLOUDINARY_URL = "${CLOUDINARY_URL}"
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
                sh 'docker container run -d --name blcapstone -p 7777:8080 -p 7778:8081 ' +
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
