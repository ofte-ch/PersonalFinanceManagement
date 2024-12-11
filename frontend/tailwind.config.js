/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      fontFamily :{
        'bad-script': ['"Bad Script"', 'cursive'], // Thêm font Bad Script
        'poppins': ['Poppins', 'sans-serif'],       // Thêm font Poppins
        'roboto': ['Roboto', 'sans-serif'],     // Thêm font Roboto
      },
      colors:{
        background: "rgba(var(--background))",
        panel: "rgba(var(--panel))",
        elements:"rgba(var(--elements))",
        primary: "rgba(var(--primary))",
        secondary: "rgba(var(--secondary))",
        border: "rgba(var(--border))",
    },
  },
  plugins: [],
  }
}

