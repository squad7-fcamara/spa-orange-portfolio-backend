-- Criar a tabela 'usuario' se não existir
CREATE TABLE IF NOT EXISTS usuario (
    id_usuario INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100),
    sobrenome VARCHAR(255),
    email VARCHAR(255) UNIQUE,
    senha VARCHAR(255)
);

-- Criar a tabela 'projeto' se também não existir
CREATE TABLE IF NOT EXISTS projeto (
    id_projeto INT PRIMARY KEY AUTO_INCREMENT,
    id_usuario INT,
    titulo VARCHAR(255),
    imagem_projeto VARCHAR(255),
    tag VARCHAR(255),
    link VARCHAR(255),
    descricao VARCHAR(255),
    data_criacao DATE,
    FOREIGN KEY (id_usuario) REFERENCES usuario(id_usuario)
);

-- Inserindo os usuários e projetos para testes
INSERT INTO usuario (nome, sobrenome, email, senha) VALUES
('João', 'Silva', 'joao.silva@email.com', 'senha1'),
('Maria', 'Santos', 'maria.santos@email.com', 'senha2'),
('Carlos', 'Oliveira', 'carlos.oliveira@email.com', 'senha3');


INSERT INTO projeto (id_usuario, titulo, imagem_projeto, tag, link, descricao, data_criacao) VALUES
(1, 'Jon Project ', 'imagem1.jpg', 'Tag1', 'https://jonproject.com', 'Descrição do Projeto do João', '2024-01-23'),
(2, 'PrograMaria', 'imagem2.jpg', 'Tag2', 'https://programaria.app', 'Descrição do Projeto da Maria', '2024-01-23'),
(3, 'Carlos Soft', 'imagem3.jpg', 'Tag3', 'https://carlosoft.org', 'Descrição do Projeto do Carlos', '2024-01-23');
