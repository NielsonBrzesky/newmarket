// @ts-nocheck
/** Declaração de variaveis */
var enderecoGerarVenda = "http://localhost:5063/Produtos/gerarvenda/";
var enderecoProduto = "http://localhost:5063/Produtos/Produto/";
var produto;
var compra = [];
var _totalVenda_ = 0.0;

/**Inicio*/
$("#idPosVenda").hide();
atualizarTotal(); 

/**Funções */
function atualizarTotal() {
	$("#totalVenda").html(_totalVenda_);
}

function preencherFormulario(dadosProduto) {
	$("#idCampoNome").val(dadosProduto.nome);
	$("#idCampoCategoria").val(dadosProduto.categoria.nome);
	$("#idCampoFornecedor").val(dadosProduto.fornecedor.nome);
	$("#idCampoPreco").val(dadosProduto.precoDeVenda);
}

function zerarFormulario() {
	$("#idCampoNome").val("");
	$("#idCampoCategoria").val("");
	$("#idCampoFornecedor").val("");
	$("#idCampoPreco").val("");
	$("#idCampoQuantidade").val("");
}

function adicionarNatabela(prod, quant) {
	var produtoTemporario = {}; //Clonando o objeto para poder mudar dentro do array.
	Object.assign(produtoTemporario, produto); //Vai fazer a cópia para não mudar o produto que está dentro do método.
	var venda = {
		produto: produtoTemporario,
		quantidade: quant,
		subtotal: produtoTemporario.precoDeVenda * quant,
	};
	_totalVenda_ += venda.subtotal;
	atualizarTotal();
	compra.push(produtoTemporario);
	$("#idCompras").append(`<tr>
		<td>${prod.id}</td>
		<td>${prod.nome}</td>
		<td>${quant}</td>
		<td>${prod.precoDeVenda} R$</td>
		<td>${prod.medicao}</td>
		<td>${prod.precoDeVenda * quant} R$</td>
		<td><button class='btn btn-danger'>Remover</button></td>
	<\tr>`);
 }

$("#idProdutoForm").on("submit", function(event) {
	event.preventDefault();//impede que o formulário carregue a página.
	var produtoParaTabela = produto;
	var quantidade = $("#idCampoQuantidade").val();
	adicionarNatabela(produtoParaTabela, quantidade);
	zerarFormulario();
});


/**Ajax da tela */
$("#pesquisar").click(function() {
	var codProduto = $("#codProduto").val();
	var enderecoTemporario = enderecoProduto + codProduto;
	$.post(enderecoTemporario, function(dados, status) {
		produto = dados;
		var medicao = "";
		switch (produto.medicao) {
			case 0:
				medicao = "Litro";
				break;
			case 1:
				medicao = "Kilo";
				break;
			case 2:
				medicao = "Unidade";
				break;
			default:
				medicao = "Unidade";
				break;
		}
		produto.medicao = medicao;
		preencherFormulario(produto);
	}).fail(function() {
		alert("Produto inválido!");
	});
});

/**Finalizar a Venda*/
$("#idFinalizarVendaButton").click(function () {
	if (_totalVenda_ <= 0) {
		alert("Compra inválida, nenhum produto foi adicionado!");
		return;
	}
	var _valorPago = $("#idValorPago").val();
	if(!isNaN(_valorPago)) {
		_valorPago = parseFloat(_valorPago);
		if(_valorPago >= _totalVenda_) {
			$("#idPosVenda").show();
			$("#idPreVenda").hide();
			$("#idValorPago").prop("disabled", true);
			var _troco = _valorPago - _totalVenda_;
			$("#idTroco").val(_troco);
			//processar o Array
			compra.forEach(elemento => {
				elemento.produto.produto.id;
			});

			//Preparar um novo objeto
			var _venda = {total: _totalVenda_, troco: _troco, produtos: compra};

			//Enviar dados para o BackEnd
			/**Requisição AJAX. Pode mandar requisição para qualquer metodo HTTP.*/
			$.ajax({
				type: "post",
				url: enderecoGerarVenda,
				dataType: "json",
				contentType: "application/json",
				data: JSON.stringify(_venda),
				success: function (data) {
					console.log("Dados enviados com sucesso!!!");
					console.log(data);
				},
			});

		} else {
			alert("Valor pago, É muito baixo!");
			return;
		}
	} else {
		alert("Valor pago, inválido!");
		return;
	}
});

function restaurarModal() {
	$("#idPosVenda").hide();
	$("#idPreVenda").show();
	$("#idValorPago").prop("disabled", false);
	$("#idTroco").val("");
	$("#idValorPago").val("");
}

$("#idFecharModal").click(function () {
	restaurarModal();
});






