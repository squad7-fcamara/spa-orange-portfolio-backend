﻿using System.ComponentModel.DataAnnotations.Schema;

namespace APISquad7.Model
{
    public class Projeto
    {
        public Projeto() { }
        public Projeto(int idUsuario, string titulo, string imagem, string tag, string link, string descricao)
        {
            IdUsuario = idUsuario;
            Titulo = titulo;
            Imagem = imagem;
            Tag = tag;
            Link = link;
            Descricao = descricao;
        }

        public int IdProjeto { get; set; }
        public int IdUsuario { get; set; }
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        public string Tag { get; set; }
        public string Link { get; set; }
        public string Descricao { get; set; }
        public string DataCriacao { get; set; }
    }
}