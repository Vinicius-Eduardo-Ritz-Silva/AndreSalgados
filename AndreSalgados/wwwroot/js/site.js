// Função para inicialização
document.addEventListener('DOMContentLoaded', function () {
    // Ativar tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Suavizar scroll para âncoras
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            document.querySelector(this.getAttribute('href')).scrollIntoView({
                behavior: 'smooth'
            });
        });
    });
});

// Configuração (ajuste esses valores conforme necessário)
const LOADING_DELAY = 800; // Mostrar loading após 800ms de espera
const MIN_SHOW_TIME = 500; // Tempo mínimo que o loading será exibido

let loadingTimeout;
let loadingStartTime;

function showLoadingDelayed(message = "Carregando...") {
    // Cancelar timeout existente
    if (loadingTimeout) clearTimeout(loadingTimeout);

    // Configurar novo timeout
    loadingTimeout = setTimeout(() => {
        loadingStartTime = Date.now();
        $('.loading-text').text(message);
        $('#loadingOverlay').addClass('active');
    }, LOADING_DELAY);
}

function hideLoadingDelayed() {
    // Cancelar timeout se ainda não foi executado
    if (loadingTimeout) clearTimeout(loadingTimeout);

    // Se o loading está visível
    if ($('#loadingOverlay').hasClass('active')) {
        const elapsed = Date.now() - loadingStartTime;
        const remaining = MIN_SHOW_TIME - elapsed;

        // Garantir que o loading seja mostrado pelo tempo mínimo
        if (remaining > 0) {
            setTimeout(() => {
                $('#loadingOverlay').removeClass('active');
            }, remaining);
        } else {
            $('#loadingOverlay').removeClass('active');
        }
    }
}

// Interceptar requisições AJAX
$(document).ajaxStart(function () {
    showLoadingDelayed();
}).ajaxComplete(function () {
    hideLoadingDelayed();
});

// Para requisições fetch
const originalFetch = window.fetch;
window.fetch = async function (...args) {
    showLoadingDelayed();
    try {
        const response = await originalFetch(...args);
        return response;
    } finally {
        hideLoadingDelayed();
    }
};

// Para operações específicas (exemplo: salvar produto)
function salvarProduto() {
    showLoadingDelayed("Salvando produto...");

    // Simular operação demorada
    setTimeout(() => {
        // Sua lógica de salvamento aqui...

        hideLoadingDelayed();
    }, 2000); // 2 segundos de exemplo
}

// Para carregamento inicial da página
$(document).ready(function () {
    showLoadingDelayed("Carregando dados...");

    $(window).on('load', function () {
        hideLoadingDelayed();
    });
});