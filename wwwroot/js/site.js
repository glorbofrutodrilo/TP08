// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function verificarRespuesta(idRespuesta, idPregunta) {
    const resultadoDiv = document.getElementById('resultado');
    const resultadoTexto = document.getElementById('resultadoTexto');
    detenerTimer();

    const respuestaClickeada = document.getElementById('respuesta');
    const esCorrecta = respuestaClickeada.getAttribute('data-correcta') === 'true';
    
    if (esCorrecta) {
        resultadoTexto.innerHTML = '<h2 style="color: #4CAF50;">¡Correcto! 🎉</h2>';
    } else {
        resultadoTexto.innerHTML = '<h2 style="color: #f44336;">Incorrecto 😞</h2>';
    }
    resultadoDiv.style.display = 'block';
}

// ===== Timer de cuenta regresiva =====
let timerTimeoutId = null;
let timerIntervalId = null;
let tiempoRestanteSegundos = 60;

function iniciarTimer(segundos = 60) {
    detenerTimer();
    tiempoRestanteSegundos = segundos;

    // Mostrar/crear el contador visual
    let contador = document.getElementById('countdown');
    if (!contador) {
        const contenedorPregunta = document.querySelector('.question-container');
        contador = document.createElement('div');
        contador.id = 'countdown';
        contador.style.marginTop = '12px';
        contador.style.fontWeight = '700';
        contador.style.color = '#fff';
        contador.style.opacity = '0.9';
        contador.style.fontSize = '1.1rem';
        if (contenedorPregunta) contenedorPregunta.appendChild(contador);
    }
    contador.textContent = `Tiempo: ${tiempoRestanteSegundos}s`;

    // Intervalo para actualizar cada segundo
    timerIntervalId = setInterval(() => {
        tiempoRestanteSegundos -= 1;
        if (tiempoRestanteSegundos < 0) return;
        const c = document.getElementById('countdown');
        if (c) c.textContent = `Tiempo: ${tiempoRestanteSegundos}s`;
    }, 1000);

    // Timeout para cuando se acaba el tiempo
    timerTimeoutId = setTimeout(() => {
        detenerTimer();
        const resultadoDiv = document.getElementById('resultado');
        const resultadoTexto = document.getElementById('resultadoTexto');
        if (resultadoTexto) resultadoTexto.innerHTML = '<h2 style="color: #f44336;">Se acabó tu tiempo ⏳</h2>';
        // Deshabilitar las respuestas
        document.querySelectorAll('.answer-button').forEach(btn => btn.disabled = true);
        if (resultadoDiv) resultadoDiv.style.display = 'block';
    }, segundos * 1000);
}

function detenerTimer() {
    if (timerTimeoutId) { clearTimeout(timerTimeoutId); timerTimeoutId = null; }
    if (timerIntervalId) { clearInterval(timerIntervalId); timerIntervalId = null; }
}

// Iniciar automáticamente el timer cuando cargue la página de juego
document.addEventListener('DOMContentLoaded', function() {
    const juego = document.querySelector('.game-container');
    if (juego) {
        iniciarTimer(60);
        // Inicializa barra de progreso usando variables de la vista (si existen)
        try {
            const total = parseInt(document.getElementById('progress-text')?.getAttribute('data-total') || '0', 10);
            const actual = parseInt(document.getElementById('progress-text')?.getAttribute('data-actual') || '0', 10);
            if (total > 0) actualizarProgreso(actual, total);
        } catch {}
    }

    // ===== Transiciones entre vistas (fade in/out) solo con JavaScript =====
    const root = document.querySelector('main[role="main"]');
    if (root) {
        // Estado inicial para animación de entrada
        root.style.opacity = '0';
        root.style.transform = 'translateY(8px)';
        root.style.transition = 'opacity 250ms ease, transform 250ms ease';
        requestAnimationFrame(() => {
            root.style.opacity = '1';
            root.style.transform = 'translateY(0)';
        });

        // Maneja clicks en enlaces internos
        document.addEventListener('click', function (e) {
            const a = e.target.closest('a');
            if (!a) return;
            const href = a.getAttribute('href');
            const target = a.getAttribute('target');
            if (!href || href.startsWith('#') || target === '_blank') return;
            // Evita transición si es un enlace a un recurso descargable
            if (a.hasAttribute('download')) return;
            // Evita transición para anchors con javascript:void(0)
            if (href.startsWith('javascript:')) return;
            e.preventDefault();
            root.style.opacity = '0';
            root.style.transform = 'translateY(8px)';
            setTimeout(() => { window.location.href = href; }, 250);
        }, true);

        // Maneja envíos de formularios visibles (sin target)
        document.addEventListener('submit', function (e) {
            const form = e.target;
            // No interceptar si el formulario tiene target (por ejemplo, verificarFrame)
            if (form.getAttribute('target')) return;
            e.preventDefault();
            root.style.opacity = '0';
            root.style.transform = 'translateY(8px)';
            setTimeout(() => { form.submit(); }, 250);
        }, true);
    }
});

// Progreso: actualiza barra y texto
function actualizarProgreso(numeroActual, total) {
    const fill = document.getElementById('progress-fill');
    const text = document.getElementById('progress-text');
    if (!fill || !text || !total) return;
    const porcentaje = Math.min(100, Math.max(0, Math.round((numeroActual / total) * 100)));
    fill.style.width = porcentaje + '%';
    text.textContent = `Progreso: ${numeroActual}/${total}`;
}