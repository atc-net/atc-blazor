export function useLightMode() {
    if (window.matchMedia) {
        const match = window.matchMedia("(prefers-color-scheme: light)");
        return match.matches;
    }

    return false;
}

export function useDarkMode() {
    if (window.matchMedia) {
        const match = window.matchMedia("(prefers-color-scheme: dark)");
        return match.matches;
    }

    return false;
}