CREATE TABLE "usuario" (
	"id_usuario" serial NOT NULL,
	"nome" varchar(100) NOT NULL,
	"sobrenome" varchar(255) NOT NULL,
	"email" varchar(255) NOT NULL UNIQUE,
	"senha" varchar(255) NOT NULL,
	CONSTRAINT "usuario_pk" PRIMARY KEY ("id_usuario")
) WITH (
  OIDS=FALSE
);

CREATE TABLE "projeto" (
	"id_projeto" serial NOT NULL,
	"id_usuario" int NOT NULL,
	"titulo" varchar(255) NOT NULL,
	"imagem_projeto" varchar(255) NOT NULL,
	"tag" varchar(255) NOT NULL,
	"link" varchar(255) NOT NULL,
	"descricao" varchar(255) NOT NULL,
	"data_criacao" DATE NOT NULL,
	CONSTRAINT "projeto_pk" PRIMARY KEY ("id_projeto")
) WITH (
  OIDS=FALSE
);

ALTER TABLE "projeto" ADD CONSTRAINT "fk_projeto_usuario" FOREIGN KEY ("id_usuario") REFERENCES "usuario"("id_usuario");

-- Inserindo os usuários e projetos para testes
INSERT INTO usuario (nome, sobrenome, email, senha) VALUES
('João', 'Silva', 'joao.silva@email.com', 'senha1'),
('Maria', 'Santos', 'maria.santos@email.com', 'senha2'),
('Carlos', 'Oliveira', 'carlos.oliveira@email.com', 'senha3');

INSERT INTO projeto (id_usuario, titulo, imagem_projeto, tag, link, descricao, data_criacao) VALUES
(1, 'Jon Project ', 'imagem1.jpg', 'Tag1', 'https://jonproject.com', 'Descrição do Projeto do João', '2024-01-23'),
(2, 'PrograMaria', 'imagem2.jpg', 'Tag2', 'https://programaria.app', 'Descrição do Projeto da Maria', '2024-01-23'),
(3, 'Carlos Soft', 'imagem3.jpg', 'Tag3', 'https://carlosoft.org', 'Descrição do Projeto do Carlos', '2024-01-23');
