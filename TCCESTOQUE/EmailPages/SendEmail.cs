using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCCESTOQUE.EmailPages
{
    public class SendEmail
    {
        public static string EmailDeVerificacao(Guid vendedorId)
        {
            return $"<!doctype html>" +
                $"<html>" +
                    $"<body>" +
                        $"<div>" +
                            $"<header>" +
                                $"<h1>E-STOCK</h1>" +
                            $"</header>" +
                            $"<main>" +
                                $"<div>" +
                                    $"<div'>" +
                                        $"<h1>Email de verificação</h1>" +
                                        $"<p>Este email esta sendo enviado para que seu cadastro no site E-Stock seja concluido, clique no botão verificar para concluir o cadastro.</p>" +
                                        $"<a href='https://localhost:44338/Vendedor/AutenticarConta?id= {vendedorId}'>Verificar</a>" +
                                     $"</div>" +
                                $"</div>" +
                            $"</main>" +
                        $"</div>" +
                    $"</body>" +
                $"</html>";
        }
        public static string EmailTrocaSenha(Guid? vendedorId, Guid trocaId, int codigo)
        {
            return $"<!doctype html>" +
                $"<html>" +
                    $"<body>" +
                        $"<div>" +
                            $"<header>" +
                                $"<h1>E-STOCK</h1>" +
                            $"</header>" +
                            $"<main>" +
                                $"<div>" +
                                    $"<div'>" +
                                        $"<h1>Troca de senha</h1>" +
                                        $"<p>Este email esta sendo enviado para que você possa trocar a senha da sua conta, clique no link abaixo para trocar a senha.</p>" +
                                        $"<a href='https://localhost:44338/Vendedor/AlterarSenha?Id= {vendedorId} &trocaId= {trocaId}'>Alterar a senha</a>" +
                                        $"<br>"+
                                        $"<label>Código para troca de senha</label>" +
                                        $"<h4>{codigo}</h4>" +
                                     $"</div>" +
                                $"</div>" +
                            $"</main>" +
                        $"</div>" +
                    $"</body>" +
                $"</html>";
        }
    }
}
