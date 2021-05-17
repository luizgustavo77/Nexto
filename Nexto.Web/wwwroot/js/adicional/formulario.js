$(document).ready(function () {
    $("IdDivDaImagem").each(function () {
        this.style.pointerEvents = 'none'; // para ativar so adicionar auto em vez de none
    });

});

$('#idInputTypeFile').fileupload({
    dataType: 'json',
    url: base_path + "/Solicitacao/UploadFiles",
    autoUpload: true,
    done: function (e, data) {
        mostrarLoading();

        if (data.result) {
            formulario().addArquivo(data.result);
        }
        else {
            removerLoading();
            alert('Houve um erro ao tentar submeter o arquivo.');
        }
    },
    error: function (error) {
        removerLoading();
        alert('Houve um erro ao tentar submeter o arquivo.');
    }
});

var arquivos = [];
var lista = [];

var formulario = function () {

    var controles = function () {
        return {
            idInputTypeFile: '#idInputTypeFile',
            select_arquivo: '#select_arquivo',
            txt_arquivo: '#txt_arquivo',
            tabela: "#tabela"
        };
    }

    var addArquivo = function (result) {
        arquivos.push(result[0]);
        $(controles().txt_arquivo).val("Arquivos adicionados.");

        removerLoading();
    }

    var salvarArquivo = function () {
        if (arquivos.length > 0) {
            var tipo = $(controles().select_arquivo + ' :selected').val();
            if (tipo && tipo > 0) {
                $.ajax({
                    type: "POST",
                    url: base_path + "/Solicitacao/SalvarArquivo",
                    data: {
                        arquivos: arquivos,
                        tipo: $(controles().select_arquivo + ' :selected').val()
                    },
                    cache: false,
                }).done(function (data) {
                    for (var i = 0; i < arquivos.length; i++) {
                        arquivos[i].id = -1 * (lista.length + 1);
                        arquivos[i].conteudo = '';
                        arquivos[i].tipo = tipo;
                        adicionar(arquivos[i]);
                    }
                    arquivos = [];
                    $(controles().txt_arquivo).val("Selecione um arquivo.");
                    $(controles().select_arquivo).val('');
                    montarTabela();
                }).fail(function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Problemas ao salvar arquivo.');
                });
            }
            else {
                alert('Selecione um tipo de arquivo.')
            }
        }
        else {
            alert('Sem arquivos para salvar.')
        }
    }

    var adicionar = function (item) {
        lista.push(item);
    }

    var deletar = function (linhaDataTable) {
        mostrarLoading();
        var dto = ExtrairObjeto(linhaDataTable, controles().tabela);

        for (var i in lista) {
            if (lista[i].id == dto.id) {
                lista.splice(i, 1);
                montarTabela();

                $.ajax({
                    type: "POST",
                    url: base_path + "/Solicitacao/DeletarArquivo",
                    data: {
                        id: lista[i].id
                    },
                    cache: false,
                }).done(function (data) {

                }).fail(function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Problemas ao deletar arquivo.');
                });

                break;
            }
        }
    }

    var pesquisar = function () {
        mostrarLoading();

        $.ajax({
            type: "POST",
            url: base_path + "/Solicitacao/GetArquivos",
            data: {
                'dto': getFiltros()
            },
            cache: false,
        }).done(function (data) {
            if (data) {
                lista = data;
                montarTabela();
            }
            else {
                removerLoading();
            }
        }).fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Problemas ao carregar a pesquisa. ' + errorThrown);
        });
    }

    var montarTabela = function () {
        $(controles().tabela).DataTable({
            responsive: true,
            dom: 'Bfrtip',
            buttons: [
                { extend: "excelHtml5", className: "buttonsToHide" }
            ],
            data: lista,
            destroy: true,
            filter: false,
            info: false,
            paginate: true,
            paginationType: 'full_numbers',
            lengthChange: false,
            iDisplayLength: 20,
            language: {
                processing: 'Processando...',
                zeroRecords: 'Nenhum registro encontrado.',
                paginate: {
                    first: '&laquo;',
                    previous: '<',
                    next: '>',
                    last: '&raquo;'
                }
            },
            order: [[0, 'asc']],
            columns: [
                {
                    data: 'nome',
                    title: 'Nome',
                    visible: true,
                    sortable: true,
                    render: function (data) {
                        if (data)
                            return data;
                        else
                            return "Sem nome";
                    }
                },
                {
                    data: 'tipo',
                    title: 'Tipo',
                    sortable: true,
                    render: function (data) {
                        if (data)
                            return $('#select_arquivo option[value="' + data + '"]').text();
                        else
                            return "Sem descrição";
                    }
                },
                {
                    data: null,
                    title: 'Ações',
                    sortable: true,
                    render: function (data) {
                        var html = "";

                        html = "<a style='cursor: pointer;' title='Deletar' data-toggle='tooltip' onclick='formulario().deletar(this); return false;' data-original-title='Deletar' style='padding:3px;'>"
                            + "🗑️</a>";
                        return html;
                    }
                }
            ]
        })

        removerLoading();
        $('.buttonsToHide').addClass('hide');
    }

    return {
        addArquivo: addArquivo,
        salvarArquivo: salvarArquivo,
        deletar: deletar,
        pesquisar: pesquisar
    }
}