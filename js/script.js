// Shared site functionality for all pages.
document.addEventListener("DOMContentLoaded", () => {
    setupMobileMenu();
    setupActiveNavLink();
    setupSmoothScrolling();
    setupScrollReveal();
    setupSlider();
    setupLadderTableToggle();
    setupContactFormValidation();
});

// Toggle mobile navigation open/close state.
function setupMobileMenu() {
    const navToggle = document.querySelector(".nav-toggle");
    const navMenu = document.getElementById("navMenu");
    const navLinks = document.querySelectorAll(".nav-link");

    if (!navToggle || !navMenu) {
        return;
    }

    navToggle.addEventListener("click", () => {
        const isOpen = navMenu.classList.toggle("open");
        navToggle.classList.toggle("open", isOpen);
        navToggle.setAttribute("aria-expanded", String(isOpen));
    });

    navLinks.forEach((link) => {
        link.addEventListener("click", () => {
            navMenu.classList.remove("open");
            navToggle.classList.remove("open");
            navToggle.setAttribute("aria-expanded", "false");
        });
    });

    document.addEventListener("keydown", (event) => {
        if (event.key === "Escape") {
            navMenu.classList.remove("open");
            navToggle.classList.remove("open");
            navToggle.setAttribute("aria-expanded", "false");
        }
    });
}

// Highlight the active page in the navbar using the current file path.
function setupActiveNavLink() {
    const currentPath = window.location.pathname.split("/").pop() || "index.html";
    const navLinks = document.querySelectorAll(".nav-link");

    navLinks.forEach((link) => {
        const linkPath = link.getAttribute("href");
        const isCurrent = linkPath === currentPath;
        link.classList.toggle("active", isCurrent);
        if (isCurrent) {
            link.setAttribute("aria-current", "page");
        } else {
            link.removeAttribute("aria-current");
        }
    });
}

// Smooth-scroll only for same-page hash links.
function setupSmoothScrolling() {
    const hashLinks = document.querySelectorAll("a[href^='#']");

    hashLinks.forEach((link) => {
        link.addEventListener("click", (event) => {
            const targetId = link.getAttribute("href");
            if (!targetId || targetId === "#") {
                return;
            }

            const targetElement = document.querySelector(targetId);
            if (!targetElement) {
                return;
            }

            event.preventDefault();
            targetElement.scrollIntoView({ behavior: "smooth", block: "start" });
        });
    });
}

// Apply reveal animation when sections enter viewport.
function setupScrollReveal() {
    if (!("IntersectionObserver" in window)) {
        document.querySelectorAll(".reveal").forEach((el) => {
            el.classList.add("in-view");
        });
        return;
    }

    const revealObserver = new IntersectionObserver(
        (entries) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    entry.target.classList.add("in-view");
                    revealObserver.unobserve(entry.target);
                }
            });
        },
        { threshold: 0.12 }
    );

    document.querySelectorAll(".reveal").forEach((el) => {
        revealObserver.observe(el);
    });
}

// Simple automatic and manual slider for the home page.
function setupSlider() {
    const slider = document.querySelector("[data-slider]");
    if (!slider) {
        return;
    }

    const slides = slider.querySelectorAll(".slide");
    const prevButton = slider.querySelector("[data-prev]");
    const nextButton = slider.querySelector("[data-next]");

    if (slides.length === 0) {
        return;
    }

    let currentIndex = 0;
    let intervalId;

    function showSlide(index) {
        slides.forEach((slide, slideIndex) => {
            slide.classList.toggle("active", slideIndex === index);
        });
    }

    function nextSlide() {
        currentIndex = (currentIndex + 1) % slides.length;
        showSlide(currentIndex);
    }

    function prevSlide() {
        currentIndex = (currentIndex - 1 + slides.length) % slides.length;
        showSlide(currentIndex);
    }

    function restartAutoPlay() {
        clearInterval(intervalId);
        intervalId = setInterval(nextSlide, 5000);
    }

    prevButton?.addEventListener("click", () => {
        prevSlide();
        restartAutoPlay();
    });

    nextButton?.addEventListener("click", () => {
        nextSlide();
        restartAutoPlay();
    });

    showSlide(currentIndex);
    restartAutoPlay();
}

// Expand and collapse league ladder rows on matches page.
function setupLadderTableToggle() {
    const toggleButton = document.querySelector("[data-ladder-toggle]");
    const ladderTable = document.querySelector("[data-ladder-table]");
    const ladderWrap = ladderTable?.closest(".table-wrap");
    const ladderRows = ladderTable?.querySelectorAll("tbody tr");

    if (!toggleButton || !ladderTable || !ladderWrap || !ladderRows || ladderRows.length <= 4) {
        if (toggleButton) {
            toggleButton.hidden = true;
        }
        return;
    }

    const firstHiddenRow = ladderRows[4];
    let isExpanded = false;

    ladderWrap.classList.add("ladder-collapsible");

    function applyLadderState() {
        const collapsedHeight = firstHiddenRow.offsetTop;
        const expandedHeight = ladderTable.scrollHeight;

        ladderWrap.classList.toggle("ladder-expanded", isExpanded);
        ladderWrap.style.maxHeight = `${isExpanded ? expandedHeight : collapsedHeight}px`;
        toggleButton.setAttribute("aria-expanded", String(isExpanded));
        toggleButton.textContent = isExpanded ? "Show Top 4 Only" : "View Full Table";
    }

    applyLadderState();

    toggleButton.addEventListener("click", () => {
        isExpanded = !isExpanded;
        applyLadderState();
    });

    window.addEventListener("resize", applyLadderState);
}

// Validate contact form fields and show inline errors.
function setupContactFormValidation() {
    const form = document.querySelector("[data-contact-form]");
    if (!form) {
        return;
    }

    const nameInput = form.querySelector("#name");
    const emailInput = form.querySelector("#email");
    const phoneInput = form.querySelector("#phone");
    const subjectInput = form.querySelector("#subject");
    const messageInput = form.querySelector("#message");
    const successMessage = form.querySelector(".form-success");

    const validators = {
        name: (value) => value.trim().length >= 2,
        email: (value) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value),
        phone: (value) => /^\+?[0-9\s-]{8,15}$/.test(value.trim()),
        subject: (value) => value.trim().length >= 3,
        message: (value) => value.trim().length >= 12
    };

    function setError(fieldId, message) {
        const input = form.querySelector(`#${fieldId}`);
        const errorBox = form.querySelector(`[data-error-for='${fieldId}']`);

        if (input) {
            input.classList.add("input-error");
            input.setAttribute("aria-invalid", "true");
        }

        if (errorBox) {
            errorBox.textContent = message;
        }
    }

    function clearError(fieldId) {
        const input = form.querySelector(`#${fieldId}`);
        const errorBox = form.querySelector(`[data-error-for='${fieldId}']`);

        if (input) {
            input.classList.remove("input-error");
            input.setAttribute("aria-invalid", "false");
        }

        if (errorBox) {
            errorBox.textContent = "";
        }
    }

    form.addEventListener("submit", (event) => {
        event.preventDefault();
        let isValid = true;

        const fields = [
            { id: "name", value: nameInput?.value || "", message: "Please enter at least 2 characters." },
            { id: "email", value: emailInput?.value || "", message: "Enter a valid email address." },
            { id: "phone", value: phoneInput?.value || "", message: "Enter a valid phone number." },
            { id: "subject", value: subjectInput?.value || "", message: "Please add a subject (min 3 chars)." },
            { id: "message", value: messageInput?.value || "", message: "Message should be at least 12 characters." }
        ];

        fields.forEach((field) => {
            if (!validators[field.id](field.value)) {
                setError(field.id, field.message);
                isValid = false;
            } else {
                clearError(field.id);
            }
        });

        if (!isValid) {
            if (successMessage) {
                successMessage.textContent = "";
            }
            return;
        }

        if (successMessage) {
            successMessage.textContent = "Thank you. Your message has been validated and is ready to send.";
        }

        form.reset();
    });
}
